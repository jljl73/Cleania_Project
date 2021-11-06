using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityMine : EnemySkill
{
    float damageScale = 10;
    float triggerRadius;
    float CreationRadius;           // 생성 반경
    float mineCount;

    public GameObject MinePond;

    [SerializeField]
    SpecialAbilityMineSO skillData;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        if (MinePond == null)
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
        triggerRadius = skillData.GetRadius();
        CreationRadius = skillData.GetCreationRadius();
        mineCount = skillData.GetCount();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSpecialSkill", true);
        animator.SetTrigger("Mine");
    }

    override public void Activate()
    {
        MakeMines();
    }

    void MakeMines()
    {
        for (int i = 1; i <= mineCount; i++)
        {
            GameObject initiatedPond = Instantiate(MinePond, transform.position, transform.rotation);
            initiatedPond.transform.position = GetRandomPointInCircle(transform.position, CreationRadius);
            Mine mine = initiatedPond.GetComponent<Mine>();
            if (mine != null)
                mine.SetUp(OwnerAbilityStatus, damageScale);
            //if (pondDamage != null)
            //{
            //    print("Pond not null");
            //    if (enemy.abilityStatus == null)
            //        print("enemy.abilityStatus is null");
            //    else
            //        print("enemy.abilityStatus not null");
            //    pondDamage.SetProperty(enemy.abilityStatus, damageScale);
            //}
            //else
            //    print("Pond null");
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
        animator.SetBool("OnSpecialSkill", false);
        animator.SetBool("OnSkill", false);
    }
}
