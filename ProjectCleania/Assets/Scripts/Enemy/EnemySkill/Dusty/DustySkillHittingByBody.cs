using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustySkillHittingByBody : EnemySkill
{
    float damageScale = 0;

    [SerializeField]
    EnemySkillSO skillData;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

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
        animator.SetFloat("HittingByBody Multiplier", SpeedMultiplier);
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("BatSkill1 no skillData");

        base.UpdateSkillData(skillData);

        damageScale = skillData.GetDamageRate();
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
            other.GetComponent<Player>().abilityStatus.AttackedBy(enemy.abilityStatus, damageScale);
        }
    }

    public override void Deactivate()
    {
        col.enabled = false;
        animator.SetBool("OnSkill", false);
    }
}
