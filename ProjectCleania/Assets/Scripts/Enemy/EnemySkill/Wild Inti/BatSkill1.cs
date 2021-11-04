using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSkill1 : EnemySkill
{
    float DamageScale = 10;
    float bloodChance = 0.3f;
    float bloodTime = 5.0f;

    Collider col;
    // Enemy enemy;

    [SerializeField]
    WildIntiSpearSO skillData;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Start()
    {
        base.Start();
        // enemy = transform.parent.parent.GetComponent<Enemy>();
        col = GetComponent<Collider>();

        UpdateSkillData();
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("BatSkill1 no skillData");

        base.UpdateSkillData(skillData);

        bloodChance = skillData.GetBloodChance();
        bloodTime = skillData.GetBloodTime();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetTrigger("Spear");
        //animator.SetInteger("Skill", 1);
    }

    override public void Activate()
    {
        col.enabled = true;

        // 출혈 디버프 결정
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Player player = other.gameObject.GetComponent<Player>();
            if (player != null)
                player.abilityStatus.AttackedBy(enemy.abilityStatus, DamageScale);
        }
    }

    public override void Deactivate()
    {
        col.enabled = false;
        animator.SetBool("OnSkill", false);
    }

}
