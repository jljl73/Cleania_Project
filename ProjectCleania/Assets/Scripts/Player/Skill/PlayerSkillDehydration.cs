using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillDehydration : PlayerSkill
{
    [SerializeField]
    PlayerSkillDehydrationSO skillData;

    public float skillScale = 1.0f;
    float timePassed = 0;

    Collider attackArea;

    // "����ġ�� ������ ���� (ex. 2.0 = 200% ������ ����)"
    float damageRate = 3.0f;
    public float GetDamageRate() { return damageRate; }

    public float damageRange = 1.0f;
    public float GetDamageRange() { return damageRange; }

    public override int ID { get { return skillData.ID; } protected set { id = value; } }
    private new void Awake()
    {
        base.Awake();

        attackArea = GetComponent<Collider>();

        UpdateSkillData();
    }

    new void Start()
    {
        //GameManager.Instance.player.OnLevelUp.AddListener(UpdateSkillData);

        base.Start();
        animator.SetFloat("Dehydration multiplier", SpeedMultiplier);

        effectController[0].Scale = damageRange * 0.5f;
    }

    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);

        damageRate = skillData.GetDamageRate();
        damageRange = skillData.GetDamageRange();
    }

    public override bool AnimationActivate()
    {
        base.AnimationActivate();

        // animator.SetInteger("Skill", 6);
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSkillR", true);
        animator.SetTrigger("Dehydration");

        return true;
    }

    public override void StopSkill()
    {
        Deactivate();
        //animator.SetTrigger("DehydrationEnd");
        effectController[0].StopSKillEffect();
    }

    public override void Activate()
    {
        if (attackArea != null)
            attackArea.enabled = true;
    }

    public override void Deactivate()
    {
        animator.SetTrigger("DehydrationEnd");
        animator.SetBool("OnSkillR", false);
        animator.SetBool("OnSkill", false);
        if (attackArea != null)
            attackArea.enabled = false;
    }

    public override void ActivateSound(int index)
    {
        GameManager.Instance.playerSoundPlayer.PlaySound(PlayerSoundPlayer.TYPE.Dehydration);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "Enemy")
        {
            timePassed += Time.deltaTime;

            if (timePassed < 1f)
                return;
            else
                timePassed = 0f;

            other.GetComponent<Enemy>().abilityStatus.AttackedBy(OwnerAbilityStatus, skillScale * Time.deltaTime);
        }
    }
}
