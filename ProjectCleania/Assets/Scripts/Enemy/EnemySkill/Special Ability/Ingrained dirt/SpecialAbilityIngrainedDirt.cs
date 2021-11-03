using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialAbilityIngrainedDirt : EnemySkill
{
    float damageScale = 10;

    [SerializeField]
    SpecialAbilityIngrainedDirtSO skillData;

    public override int ID { get { return skillData.ID; } protected set { id = value; } }
    private new void Awake()
    {
        base.Awake();
    }

    private new void Start()
    {
        base.Start();

        UpdateSkillData();
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("SpecialAbilityIngrainedDirt no skillData");

        ID = skillData.ID;
        SkillName = skillData.GetSkillName();
        SkillDetails = skillData.GetSkillDetails();
        CoolTime = skillData.GetCoolTime();
        CreatedMP = skillData.GetCreatedMP();
        ConsumMP = skillData.GetConsumMP();
        SpeedMultiplier = skillData.GetSpeedMultiplier();

        //stainRadius = skillData.GetStainRadius();
        //stainAvailableAreaRadius = skillData.GetCreationRadius();
        //stainCount = skillData.GetCount();
        //stopTime = skillData.GetStopTime();
        //projFlightTime = skillData.GetProjFlightTime();
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
