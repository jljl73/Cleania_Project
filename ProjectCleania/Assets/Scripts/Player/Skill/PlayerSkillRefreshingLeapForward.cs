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

    // "내려치기 데미지 비율 (ex. 2.0 = 200% 데미지 적용)"
    float smashDamageRate = 5.4f;
    public float GetSmashDamageRate() { return smashDamageRate; }

    // "내려치기 범위"
    float smashRange = 2f;
    public float GetSmashRange() { return smashRange; }

    // 칼 휘두름 크기
    float swingDownSize = 1f;
    public float GetSwingDownSize() { return swingDownSize; }

    // "경직 시간"
    float stunTime = 1.5f;
    public float GetStunTime() { return stunTime; }

    // "슬로우 시간"
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

        //GameManager.Instance.player.OnLevelUp.AddListener(UpdateSkillData);
        attackArea = GetComponent<CapsuleCollider>();

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

        Physics.IgnoreLayerCollision(3, 6);

        return true;
    }

    override public void Activate()
    {
        // Attaking으로 상태 전환
        base.Activate();

        if (attackArea != null)
            attackArea.enabled = true;

        Camera.main.GetComponent<CinemachineVirtualCameraManager>().CameraShakeBegin();
    }

    public override void Deactivate()
    {
        Physics.IgnoreLayerCollision(3, 6, false);
        if (attackArea != null)
            attackArea.enabled = false;
    }

    public override void ActivateSound(int index)
    {
        switch (index)
        {
            case 0:
                GameManager.Instance.playerSoundPlayer.PlaySound(PlayerSoundPlayer.TYPE.RefreshingLeapForward, 0);
                break;
            case 1:
                GameManager.Instance.playerSoundPlayer.PlaySound(PlayerSoundPlayer.TYPE.RefreshingLeapForward, 1);
                break;
            default:
                break;
        }
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
