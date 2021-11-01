using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSkill3 : EnemySkill
{
    float DamageScale = 10;
    float hitForce = 1;

    Collider col;

    [SerializeField]
    public DustWindSO skillData;

    public override int ID { get { return skillData.ID; } protected set { id = value; } }
    private new void Awake()
    {
        base.Awake();
    }

    private new void Start()
    {
        base.Start();
        col = GetComponent<Collider>();
        UpdateSkillData();
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
        hitForce = skillData.GetHitForce();
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
