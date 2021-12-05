using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSkill1 : EnemySkill
{
    float damageRange = 1f;
    float damageScale = 10;
    float bloodChance = 0.3f;
    float bloodTime = 5.0f;

    BoxCollider col;
    // Enemy enemy;

    [SerializeField]
    WildIntiSpearSO skillData;

    public override bool IsPassiveSkill { get { return skillData.GetIsPassiveSkill(); } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    protected new void Awake()
    {
        base.Awake();
        col = GetComponent<BoxCollider>();
    }

    private new void Start()
    {
        base.Start();

        UpdateSkillData();
        col.center = triggerPosition;
        col.size = new Vector3(triggerRange, triggerRange, triggerRange);

        animator.SetFloat("Spear multiplier", SpeedMultiplier);
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("BatSkill1 no skillData");

        base.UpdateSkillData(skillData);

        damageRange = triggerRange;
        damageScale = skillData.GetDamageRate();
        bloodChance = skillData.GetBloodChance();
        bloodTime = skillData.GetBloodTime();
    }

    public override bool AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetTrigger("Spear");
        //animator.SetInteger("Skill", 1);

        return true;
    }

    override public void Activate()
    {
        //col.enabled = true;
        Attack();
        // 출혈 디버프 결정
    }

    void Attack()
    {
        Collider[] colliders = Physics.OverlapBox(transform.position + triggerPosition, new Vector3(damageRange, damageRange, damageRange));
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                Player player = colliders[i].GetComponent<Player>();
                player.abilityStatus.AttackedBy(OwnerAbilityStatus, damageScale);
            }
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        //col.enabled = false;
        animator.SetBool("OnSkill", false);
    }

}
