using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggletSkill1 : EnemySkill
{
    float DamageScale = 0;

    [SerializeField]
    EnemySkillSO skillData;

    Collider col;
    // Enemy enemy;

    private new void Awake()
    {
        base.Awake();
        UpdateSkillData();
    }

    private new void Start()
    {
        base.Start();

        col = GetComponent<Collider>();
        // enemy = transform.parent.parent.GetComponent<Enemy>();
    }

    public void UpdateSkillData()
    {
        ID = skillData.ID;
        SkillName = skillData.GetSkillName();
        SkillDetails = skillData.GetSkillDetails();
        CoolTime = skillData.GetCoolTime();
        CreatedMP = skillData.GetCreatedMP();
        ConsumMP = skillData.GetConsumMP();
        SpeedMultiplier = skillData.GetSpeedMultiplier();

        DamageScale = skillData.GetDamageRate();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetTrigger("HittingByBody");
    }

    override public void Activate()
    {
        col.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().abilityStatus.AttackedBy(enemy.abilityStatus, DamageScale);
        }
    }

    public override void Deactivate()
    {
        col.enabled = false;
        animator.SetBool("OnSkill", false);
    }
}
