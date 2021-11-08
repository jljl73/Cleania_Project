using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialAbilityStain : EnemySkill
{
    float damageScale = 10;
    float stainRadius;
    float stainAvailableAreaRadius;           // 생성 반경
    float stainCount;
    float stopTime;
    float projFlightTime;
    float destroyAttackRange;
    float destroyAttackScale;

    public GameObject StainProjectilePrefab;

    [SerializeField]
    SpecialAbilityStainSO skillData;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        if (StainProjectilePrefab == null)
            throw new System.Exception("SpecialAbilityToxicity doesnt have DustPond");
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

        base.UpdateSkillData(skillData);

        damageScale = skillData.GetDamageRate();
        stainRadius = skillData.GetStainRadius();
        stainAvailableAreaRadius = skillData.GetCreationRadius();
        stainCount = skillData.GetCount();
        stopTime = skillData.GetStopTime();
        projFlightTime = skillData.GetProjFlightTime();
        destroyAttackRange = skillData.GetDestroyAttackRange();
        destroyAttackScale = skillData.GetDestroyAttackScale();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSpecialSkill", true);
        animator.SetTrigger("Stain");
    }

    override public void Activate()
    {
        ShotStain();
    }

    void ShotStain()
    {
        for (int i = 1; i <= stainCount; i++)
        {
            Shot();
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

    void Shot()
    {
        GameObject obj = Instantiate(StainProjectilePrefab, this.transform.position, Quaternion.identity);
        if (obj == null) return;

        StainProjectile stainProj = obj.GetComponent<StainProjectile>();
        if (stainProj == null) return;

        Rigidbody rigidbody = obj.GetComponent<Rigidbody>(); ;
        if (rigidbody != null)
        {
            Vector3 targetPos = GetRandomPointInCircle(transform.position, stainAvailableAreaRadius);
            rigidbody.velocity = CaculateVelocity(targetPos, transform.position, projFlightTime);
            stainProj.SetUp(stopTime, projFlightTime * 0.5f, destroyAttackRange, destroyAttackScale, OwnerAbilityStatus, damageScale);
        }
    }

    Vector3 CaculateVelocity(Vector3 target, Vector3 origin, float time)
    {
        Vector3 distance = target - origin;
        Vector3 distanceXZ = distance;
        distanceXZ.y = 0f;

        float Sy = distance.y;
        float Sxz = distanceXZ.magnitude;

        float Vxz = Sxz / time;
        float Vy = Sy / time + 0.5f * Mathf.Abs(Physics.gravity.y) * time;

        Vector3 result = distanceXZ.normalized;
        result *= Vxz;
        result.y = Vy;

        return result;
    }
}
