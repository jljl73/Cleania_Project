using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSweeping : PlayerSkill
{
    [SerializeField]
    PlayerSkillSweepingSO skillData;
    float damageScale = 0.0f;

    CapsuleCollider col;

    // "���� �ð�"
    float stunTime = 2;
    public float GetStunTime() { return stunTime; }

    // "������ ����"
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
        //GameManager.Instance.player.OnLevelUp.AddListener(UpdateSkillData);
        animator.SetFloat("Sweeping multiplier", SpeedMultiplier);

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
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            // ����
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
