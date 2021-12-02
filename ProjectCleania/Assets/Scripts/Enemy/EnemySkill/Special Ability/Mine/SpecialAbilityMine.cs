using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityMine : EnemySkill
{
    float damageScale = 10;
    float triggerRadius;
    float CreationRadius;           // 생성 반경
    float mineCount;

    //public GameObject MinePond;

    [SerializeField]
    SpecialAbilityMineSO skillData;

    SphereCollider triggerCollider;

    public override bool IsPassiveSkill { get { return skillData.GetIsPassiveSkill(); } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        triggerCollider = GetComponent<SphereCollider>();
        //if (MinePond == null)
        //    throw new System.Exception("SpecialAbilityToxicity doesnt have DustPond");
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
        triggerRadius = skillData.GetRadius();
        CreationRadius = skillData.GetCreationRadius();
        mineCount = skillData.GetCount();
    }

    public override bool AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSpecialSkill", true);
        animator.SetTrigger("Mine");

        return true;
    }

    override public void Activate()
    {
        MakeMines();
    }

    void MakeMines()
    {
        for (int i = 1; i <= mineCount; i++)
        {
            Mine mine = ObjectPool.SpawnFromPool<Mine>(ObjectPool.enumPoolObject.Mine, GetRandomPointInCircle(transform.position, CreationRadius), transform.rotation);
            mine.SetUp(OwnerAbilityStatus, damageScale);
            mine.Resize(triggerRadius);
        }
    }

    Vector3 GetRandomPointInCircle(Vector3 center, float distance)
    {
        Vector3 randomPos = Random.insideUnitSphere * distance + center;
        UnityEngine.AI.NavMeshHit hit;
        if (UnityEngine.AI.NavMesh.SamplePosition(randomPos, out hit, distance, UnityEngine.AI.NavMesh.AllAreas))
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
