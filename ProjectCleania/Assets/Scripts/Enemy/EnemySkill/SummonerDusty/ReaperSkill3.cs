using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSkill3 : EnemySkill
{
    float damageScale = 10;
    float damageRange = 1.5f;
    float hitForce = 1;
    float pushRadius;

    bool hasAttacked = false;

    SphereCollider col;

    [SerializeField]
    public DustWindSO skillData;

    public override bool IsPassiveSkill { get { return skillData.GetIsPassiveSkill(); } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        col = GetComponent<SphereCollider>();
        if (col == null)
            print("im in reaperskill3. col is null");
    }

    private new void Start()
    {
        base.Start();
        
        UpdateSkillData();
        col.center = triggerPosition;
        col.radius = triggerRange;

        effectController[0].Scale = pushRadius;
        animator.SetFloat("SpinAttack multiplier", SpeedMultiplier);
    }

    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);

        damageScale = skillData.GetDamageRate();
        damageRange = triggerRange;
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
        if (hasAttacked)
            return;
        Push();
        hasAttacked = true;
    }

    void Push()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                PlayerController player = colliders[i].GetComponent<PlayerController>();
                player.GetComponent<AbilityStatus>()?.AttackedBy(OwnerAbilityStatus, damageScale);

                player.Pushed(Vector3.Normalize(player.transform.position - this.transform.position) * hitForce);
            }
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();

        animator.SetBool("OnSkill", false);

        hasAttacked = false;
    }
}
