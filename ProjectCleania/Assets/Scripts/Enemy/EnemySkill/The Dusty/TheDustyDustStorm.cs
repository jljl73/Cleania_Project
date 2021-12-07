using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDustyDustStorm : EnemySkill
{
    [SerializeField]
    DustStormSO skillData;

    SphereCollider skillTriggerCollider;

    float damageScale;
    float damageRadius = 1;
    float pulledSpeed = 5;
    float stormDuration = 5;
    float stormDamageRate = 1;
    float stormDamageRadius = 1;
    float stormForce = 100f;
    float sightHindRange = 0.3f;
    float sightHindDuration = 4f;
    float triggerProbability = 0.3f;

    AttackType attackType = AttackType.None;
    enum AttackType
    {
        None,
        BreatheIn,
        BreatheOut
    }

    public override bool IsPassiveSkill { get { return skillData.GetIsPassiveSkill(); } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        skillTriggerCollider = GetComponent<SphereCollider>();
    }

    private void OnEnable()
    {
        Start();
    }

    private new void Start()
    {
        base.Start();

        //breatheOutAttackCollider.enabled = false;

        UpdateSkillData();
        skillTriggerCollider.center = triggerPosition;
        skillTriggerCollider.radius = triggerRange;
    }

    private void Update()
    {
        AttackByState();
    }
    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("TheDustyDustStorm no skillData");

        base.UpdateSkillData(skillData);
        damageScale = skillData.GetDamageRate();
        damageRadius = skillData.GetDamageRadius();
        pulledSpeed = skillData.GetPulledSpeed();
        stormDuration = skillData.GetstormDuration();
        stormDamageRate = skillData.GetStormDamageRate();
        stormDamageRadius = skillData.GetStormDamageRadius();
        stormForce = skillData.GetStormForce();
        sightHindRange = skillData.GetSightHindRange();
        sightHindDuration = skillData.GetsightHindDuration();
        triggerProbability = skillData.GetTriggerProbability();
    }

    public override bool AnimationActivate()
    {
        if (!(Random.Range(0.0f, 1.0f) <= triggerProbability))
            return false;

        animator.SetBool("OnSkill", true);
        animator.SetTrigger("DustStorm");
        return true;
    }

    public override void Activate(int idx)
    {
        base.Activate();

        switch (idx)
        {
            case 0:
                attackType = AttackType.BreatheIn;
                break;
            case 1:
                attackType = AttackType.BreatheOut;
                break;
            default:
                break;
        }
    }

    public override void Deactivate(int idx)
    {
        switch (idx)
        {
            case 0:
                attackType = AttackType.None;
                DoBreatheInAttack(false);
                break;
            case 1:
                attackType = AttackType.None;
                base.Deactivate();
                animator.SetBool("OnSkill", false);
                break;
            default:
                break;
        }
        return;
    }

    void AttackByState()
    {
        switch (attackType)
        {
            case AttackType.None:
                //breatheOutAttackCollider.enabled = false;
                break;
            case AttackType.BreatheIn:
                //breatheOutAttackCollider.enabled = false;
                DoBreatheInAttack(true);
                break;
            case AttackType.BreatheOut:
                //breatheOutAttackCollider.enabled = true;
                DoBreatheOutAttack();
                break;
            default:
                break;
        }
    }

    void DoBreatheInAttack(bool value)
    {
        Collider[] colliders = Physics.OverlapSphere(GetWorldTriggerPosition(triggerPosition), damageRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                PlayerController player = colliders[i].GetComponent<PlayerController>();
                player.Pulled(value, this.transform.position);
            }
        }
    }

    void DoBreatheOutAttack()
    {
        Collider[] colliders = Physics.OverlapCapsule(GetWorldTriggerPosition(new Vector3(0, 1, 2.75f)),
                                                      GetWorldTriggerPosition(new Vector3(0, 1, 2.75f + 4)),
                                                      1);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                PlayerController player = colliders[i].GetComponent<PlayerController>();
                Vector3 hitVector = Vector3.Normalize(colliders[i].transform.position - this.transform.position) * stormForce;
                player.Pushed(hitVector);
            }
        }
    }

    //private void OnDrawGizmos()
    //{
    //    if (attackType == AttackType.BreatheOut)
    //    {
    //        Gizmos.DrawSphere(GetWorldTriggerPosition(new Vector3(0, 1, 2.75f + 4 * animator.GetCurrentAnimatorClipInfo(0).Length)), 1);
    //        print("animator.GetCurrentAnimatorClipInfo(0).Length): " + animator.GetCurrentAnimatorClipInfo(0).Length);

    //    }
    //}

    //private void OnTriggerStay(Collider other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        Player player = other.GetComponent<Player>();
    //        Vector3 hitVector = Vector3.Normalize(other.transform.position - this.transform.position) * stormForce;
    //        player.playerMove.AddForce(hitVector);
    //    }
    //}
}
