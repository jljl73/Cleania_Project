using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialAbilityTurret : EnemySkill
{
    float damageScale = 10;
    float duration;
    float creationRadius;
    int count;
    float shotInterval;
    float shotRange;
    float projectileSpeed;

    [SerializeField]
    GameObject turretPrefab;

    [SerializeField]
    SpecialAbilityTurretSO skillData;

    public override int ID { get { return skillData.ID; } protected set { id = value; } }
    private new void Awake()
    {
        base.Awake();
        if (turretPrefab == null)
            throw new System.Exception("SpecialAbilityTurret doesnt have turretPrefab");
    }

    private new void Start()
    {
        base.Start();

        UpdateSkillData();
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("BatSkill1 no skillData");

        ID = skillData.ID;
        SkillName = skillData.GetSkillName();
        SkillDetails = skillData.GetSkillDetails();
        CoolTime = skillData.GetCoolTime();
        CreatedMP = skillData.GetCreatedMP();
        ConsumMP = skillData.GetConsumMP();
        SpeedMultiplier = skillData.GetSpeedMultiplier();

        damageScale = skillData.GetDamageRate();
        duration = skillData.GetDuration();
        creationRadius = skillData.GetCreationRadius();
        count = skillData.GetCount();
        shotInterval = skillData.GetShotInterval();
        shotRange = skillData.GetShotRange();
        projectileSpeed = skillData.GetProjectileSpeed();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSpecialSkill", true);
        animator.SetTrigger("Turret");
    }

    override public void Activate()
    {
        MakeTurret();
    }

    void MakeTurret()
    {
        print("Made orbit!");
        for (int i = 0; i < count; i++)
        {
            GameObject initiatedOrbit = Instantiate(turretPrefab, GetRandomPointInCircle(transform.position, creationRadius), transform.rotation);
            Turret turret = initiatedOrbit.GetComponent<Turret>();
            if (turret != null)
                turret.SetUp(enemyMove.TargetObject, shotInterval, shotRange, projectileSpeed);
            // float shotInterval;
            // float shotRange;
            // float projectileSpeed;

            Destroy(this.gameObject, duration);
        }
    }

    Vector3 GetRandomPointInCircle(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPos, out hit, distance, NavMesh.AllAreas))
            return hit.position;
        else
            return center;
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSpecialSkill", false);
        animator.SetBool("OnSkill", false);
    }
}
