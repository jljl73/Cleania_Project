using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialAbilityStormWind : EnemySkill
{
    [SerializeField]
    SpecialAbilityStormWindSO skillData;
    
    float damageScale = 10;
    float duration;
    float orbitOffset;
    float orbitCount;
    float rotationSpeed; // m/s
    float damageSize;

    //[SerializeField]
    //GameObject stormWindOrbitPrefab;

    SphereCollider triggerCollider;


    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        //if (stormWindOrbitPrefab == null)
        //    throw new System.Exception("SpecialAbilityStormWind doesnt have stormWindOrbitPrefab");
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
        orbitOffset = skillData.GetOrbitOffset();
        orbitCount = skillData.GetCount();
        rotationSpeed = skillData.GetSpeed();
        damageSize = skillData.GetDamageSize();
    }

    public override bool AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSpecialSkill", true);
        animator.SetTrigger("StormWind");

        return true;
    }

    override public void Activate()
    {
        MakeOrbit();
    }

    void MakeOrbit()
    {
        print("Made orbit!");
        for (int i = 1; i <= orbitCount; i++)
        {
            //GameObject initiatedOrbit = Instantiate(stormWindOrbitPrefab, transform.position, transform.rotation);
            //StormWindController orbitController = initiatedOrbit.GetComponent<StormWindController>();
            StormWindController orbitController = ObjectPool.SpawnFromPool<StormWindController>(ObjectPool.enumPoolObject.StormWindGroup, transform.position, transform.rotation);
            orbitController.SetUp(i % 2 == 1, i * orbitOffset, rotationSpeed, i, duration, OwnerAbilityStatus, damageScale, damageSize);
            //Destroy(this.gameObject, duration);
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
