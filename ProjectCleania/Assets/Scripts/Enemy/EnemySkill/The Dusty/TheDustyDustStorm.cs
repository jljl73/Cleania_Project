using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TheDustyDustStorm : EnemySkill
{
    [SerializeField]
    DustStormSO skillData;

    CapsuleCollider breatheOutAttackCollider;

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

    bool isBreatheInAttackPlaying = false;
    bool isBreatheOutAttackPlaying = false;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        breatheOutAttackCollider = GetComponent<CapsuleCollider>();
    }

    private void OnEnable()
    {
        Start();
    }

    private new void Start()
    {
        base.Start();

        breatheOutAttackCollider.enabled = false;

        UpdateSkillData();
    }

    private void FixedUpdate()
    {
        if (isBreatheInAttackPlaying)
            DoBreatheInAttack(true);

        if (isBreatheOutAttackPlaying)
        {
            breatheOutAttackCollider.enabled = true;
            //breatheOutAttackCollider.center = new Vector3(0, 1, 1.75f);
            //breatheOutAttackCollider.height = 2;
        }
        else
        {
            breatheOutAttackCollider.enabled = false;
            //breatheOutAttackCollider.center = new Vector3(0, 1, 1.75f);
            //breatheOutAttackCollider.height = 2;
        }
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
                isBreatheInAttackPlaying = true;
                break;
            case 1:
                isBreatheOutAttackPlaying = true;
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
                isBreatheInAttackPlaying = false;
                DoBreatheInAttack(false);
                break;
            case 1:
                isBreatheOutAttackPlaying = false;
                enemy.enemyStateMachine.Transition(StateMachine.enumState.Idle);
                animator.SetBool("OnSkill", false);
                break;
            default:
                break;
        }
        return;
    }

    void DoBreatheInAttack(bool value)
    {
        Collider[] colliders = Physics.OverlapSphere(this.transform.position, damageRadius);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].CompareTag("Player"))
            {
                Player player = colliders[i].GetComponent<Player>();
                player.playerMove.Pulled(value, this.transform.position);
                print("player pulled: " + value);
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Player player = other.GetComponent<Player>();
            Vector3 hitVector = Vector3.Normalize(other.transform.position - this.transform.position) * stormForce;
            player.playerMove.AddForce(hitVector);
        }
    }
}
