using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSkill3 : EnemySkill
{
    float damageScale = 10;
    float hitForce = 1;
    float pushRadius;

    CapsuleCollider col;

    [SerializeField]
    public DustWindSO skillData;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
    }

    private new void Start()
    {
        base.Start();
        col = GetComponent<CapsuleCollider>();
        if (col != null)
            print("im in reaperskill3. col is not null");
        else
            print("im in reaperskill3. col is null");
        UpdateSkillData();

        effectController[0].Scale = pushRadius;
        animator.SetFloat("SpinAttack multiplier", SpeedMultiplier);
    }

    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);

        damageScale = skillData.GetDamageRate();
        hitForce = skillData.GetHitForce();
        pushRadius = skillData.GetPushRadius();
    }

    public override bool AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetTrigger("SpinAttack");

        return true;
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
            other.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(other.transform.position - this.transform.position) * hitForce);
            //other.GetComponent<Player>()
        }
    }

    public override void Deactivate()
    {
        if (col == null)
            print("col is null!");
        col.enabled = false;
        animator.SetBool("OnSkill", false);
    }
}
