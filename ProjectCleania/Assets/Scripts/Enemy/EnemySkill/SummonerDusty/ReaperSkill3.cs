using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReaperSkill3 : EnemySkill
{
    float damageScale = 10;
    float damageRange = 1.5f;
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
        //col.enabled = true;
        Push();
    }

    void Push()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, damageRange);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                Player player = colliders[i].GetComponent<Player>();
                player.abilityStatus.AttackedBy(OwnerAbilityStatus, damageScale);
                player.GetComponent<Rigidbody>().AddForce(Vector3.Normalize(player.transform.position - this.transform.position) * hitForce);
            }
        }
    }

    public override void Deactivate()
    {
        base.Deactivate();
        //if (col != null)
        //    col.enabled = false;
        animator.SetBool("OnSkill", false);
    }
}
