using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSweeping : PlayerSkill
{
    [SerializeField]
    PlayerSkillSweepingSO skillData;
    float damageScale = 0.0f;

    CapsuleCollider col;

    // "경직 시간"
    float stunTime = 2;
    public float GetStunTime() { return stunTime; }

    // "쓸어담기 범위"
    float sweepRange = 2f;
    public float GetSweepRange() { return sweepRange; }

    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        col = GetComponent<CapsuleCollider>();
        UpdateSkillData();
    }

    protected new void Start()
    {
        base.Start();

        effectController[0].Scale = sweepRange * 0.3333f;
    }
    
    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);

        stunTime = skillData.GetStunTime();
        sweepRange = skillData.GetSweepRange();
        damageScale = skillData.GetDamageScale();
    }

    override public void Activate()
    {
        col.enabled = true;
        Camera.main.GetComponent<CinemachineVirtualCameraManager>().CameraShakeBegin();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // 기절
            other.GetComponent<Enemy>()?.OnStunned(true, stunTime);
            other.GetComponent<AbilityStatus>()?.AttackedBy(OwnerAbilityStatus, damageScale);
        }
    }

    void OffSkill()
    {
        if (col != null)
            col.enabled = false;
    }

    public override void Deactivate()
    {
        OffSkill();
    }

    public override void ActivateSound(int index)
    {
        GameManager.Instance.playerSoundPlayer.PlaySound(PlayerSoundPlayer.TYPE.Sweeping);
    }
}
