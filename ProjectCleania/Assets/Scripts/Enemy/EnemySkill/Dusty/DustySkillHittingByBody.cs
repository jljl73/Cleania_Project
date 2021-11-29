using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DustySkillHittingByBody : EnemySkill
{
    [SerializeField]
    EnemySkillSO skillData;
    
    float damageScale = 0;
    float damageRange = 1;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    BoxCollider col;
    // Enemy enemy;

    private new void Awake()
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

        animator.SetFloat("HittingByBody Multiplier", SpeedMultiplier);
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("BatSkill1 no skillData");

        base.UpdateSkillData(skillData);

        damageScale = skillData.GetDamageRate();
        damageRange = triggerRange;
    }

    public override bool AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetTrigger("HittingByBody");

        return true;
    }

    override public void Activate()
    {
        //col.enabled = true;
        Attack();
    }

    void Attack()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position + triggerPosition, damageRange + 0.5f);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                Player player = colliders[i].GetComponent<Player>();
                player.abilityStatus.AttackedBy(OwnerAbilityStatus, damageScale);
            }
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        other.GetComponent<Player>().abilityStatus.AttackedBy(enemy.abilityStatus, damageScale);
    //    }
    //}

    public override void Deactivate()
    {
        base.Deactivate();
        //col.enabled = false;
        animator.SetBool("OnSkill", false);
    }
}
