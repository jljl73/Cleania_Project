using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialAbilityTurret : EnemySkill
{
    [SerializeField]
    SpecialAbilityTurretSO skillData;
    
    float damageScale = 10;
    float duration;
    float creationRadius;
    int count;
    float projectileDuration;
    float shotInterval;
    float shotRange;
    float projectileSpeed;

    [SerializeField]
    GameObject turretPrefab;


    SphereCollider triggerCollider;

    public override bool IsPassiveSkill { get { return skillData.GetIsPassiveSkill(); } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        triggerCollider = GetComponent<SphereCollider>();
        //if (turretPrefab == null)
        //    throw new System.Exception("SpecialAbilityTurret doesnt have turretPrefab");
    }

    private new void Start()
    {
        base.Start();

        UpdateSkillData();
        triggerCollider.center = triggerPosition;
        triggerCollider.radius = triggerRange;
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("BatSkill1 no skillData");

        base.UpdateSkillData(skillData);

        damageScale = skillData.GetDamageRate();
        duration = skillData.GetDuration();
        creationRadius = skillData.GetCreationRadius();
        count = skillData.GetCount();
        projectileDuration = skillData.GetProjectileDuration();
        shotInterval = skillData.GetShotInterval();
        shotRange = skillData.GetShotRange();
        projectileSpeed = skillData.GetProjectileSpeed();
    }

    public override bool AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSpecialSkill", true);
        animator.SetTrigger("Turret");

        return true;
    }

    override public void Activate()
    {
        MakeTurret();
    }

    void DeactivateDelay() => this.gameObject.SetActive(false);

    void MakeTurret()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject initiatedOrbit = Instantiate(turretPrefab, GetRandomPointInCircle(transform.position, creationRadius), transform.rotation);
            // Turret turret = ObjectPool.SpawnFromPool<Turret>(ObjectPool.enumPoolObject.Turret, GetRandomPointInCircle(transform.position, creationRadius), transform.rotation);
            initiatedOrbit.GetComponent<Turret>()?.SetUp(duration, projectileDuration, enemyMove.TargetObject, shotInterval, shotRange, projectileSpeed, OwnerAbilityStatus, damageScale);

            // Destroy(this.gameObject, duration);
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
        base.Deactivate();
        animator.SetBool("OnSpecialSkill", false);
        animator.SetBool("OnSkill", false);
    }
}
