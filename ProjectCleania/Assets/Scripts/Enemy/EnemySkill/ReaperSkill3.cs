using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSkill3 : EnemySkill
{
    float DamageScale = 10;
    float hitForce = 1;

    Collider col;

    public DustWindSO SkillData;

    private new void Awake()
    {
        base.Awake();
        UpdateSkillData();
    }

    private new void Start()
    {
        base.Start();

        col = GetComponent<Collider>();
    }

    public void UpdateSkillData()
    {
        SkillName = SkillData.GetSkillName();
        SkillDetails = SkillData.GetSkillDetails();
        CoolTime = SkillData.GetCoolTime();
        CreatedMP = SkillData.GetCreatedMP();
        ConsumMP = SkillData.GetConsumMP();
        SpeedMultiplier = SkillData.GetSpeedMultiplier();

        DamageScale = SkillData.GetDamageRate();
        hitForce = SkillData.GetHitForce();

    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetTrigger("SpinAttack");
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
            other.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(other.transform.position - this.transform.position) * hitForce);
            //other.GetComponent<Player>()
        }
    }

    public override void Deactivate()
    {
        col.enabled = false;
        animator.SetBool("OnSkill", false);
    }
}
