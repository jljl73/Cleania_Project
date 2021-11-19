using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerSkillRefreshingLeapForward : PlayerSkill
{
    [SerializeField]
    PlayerSkillRefreshingLeapForwardSO skillData;

    float jumpDistance = 7f;
    public float GetJumpDistance() { return jumpDistance; }

    float jumpEffectSize = 1f;
    public float GetJumpEffectSize() { return jumpEffectSize; }

    // "����ġ�� ������ ���� (ex. 2.0 = 200% ������ ����)"
    float smashDamageRate = 5.4f;
    public float GetSmashDamageRate() { return smashDamageRate; }

    // "����ġ�� ����"
    float smashRange = 2f;
    public float GetSmashRange() { return smashRange; }

    // Į �ֵθ� ũ��
    float swingDownSize = 1f;
    public float GetSwingDownSize() { return swingDownSize; }

    // "���� �ð�"
    float stunTime = 1.5f;
    public float GetStunTime() { return stunTime; }

    // "���ο� �ð�"
    float slowTime = 2f;
    public float GetSlowTime() { return slowTime; }

    Ray ray;
    RaycastHit hit;
    CapsuleCollider attackArea;
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        UpdateSkillData();
    }

    new void Start()
    {
        base.Start();

        GameManager.Instance.player.OnLevelUp += UpdateSkillData;
        attackArea = GetComponent<CapsuleCollider>();
        animator.SetFloat("RefreshingLeapForward mulitplier", SpeedMultiplier);

        attackArea.radius = 1.2f * smashRange;

        effectController[0].Scale = jumpEffectSize;
        effectController[1].Scale = swingDownSize * 0.3333f;
        effectController[2].Scale = smashRange;

        effectController[0].MovePosition(new Vector3(0, 0, -1.5f * jumpEffectSize));
    }

    public void UpdateSkillData()
    {
        UpdateSkillData(skillData);

        jumpDistance = skillData.GetJumpDistance();
        jumpEffectSize = skillData.GetJumpEffectSize();
        smashDamageRate = skillData.GetSmashDamageRate();
        smashRange = skillData.GetSmashRange();
        swingDownSize = skillData.GetSwingDownSize();
        stunTime = skillData.GetStunTime();
        slowTime = skillData.GetSlowTime();
    }

    public override bool AnimationActivate()
    {
        base.AnimationActivate();

        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkill4", true);
        animator.SetTrigger("RefreshingLeapForward");
        Physics.IgnoreLayerCollision(3, 6);

        return true;
    }

    override public void Activate()
    {
        if (attackArea != null)
            attackArea.enabled = true;
    }

    public override void Deactivate()
    {
        Physics.IgnoreLayerCollision(3, 6, false);
        animator.SetBool("OnSkill4", false);
        animator.SetBool("OnSkill", false);
        attackArea.enabled = false;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            Enemy enemy = other.GetComponent<Enemy>();
            AbilityStatus enemyAbil = other.GetComponent<Enemy>().abilityStatus;
            if (enemyAbil.HP != 0)
            {
                enemyAbil.AttackedBy(OwnerAbilityStatus, smashDamageRate);
                enemy.OnStunned(true, stunTime);
            }
        }
    }
}
