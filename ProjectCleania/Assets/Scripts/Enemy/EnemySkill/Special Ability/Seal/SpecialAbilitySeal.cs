using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialAbilitySeal : EnemySkill
{
    float damageScale = 10;
    float duration;
    float radius;
    float CreationRadius;           // 생성 반경
    float pondCount;
    float silenceTime;
    // 침묵 상태 이상

    public GameObject SealPond;

    [SerializeField]
    SpecialAbilitySealSO skillData;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        if (SealPond == null)
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
        duration = skillData.GetDuration();
        radius = skillData.GetRadius();
        CreationRadius = skillData.GetCreationRadius();
        pondCount = skillData.GetCount();
        silenceTime = skillData.GetSilenceTime();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSpecialSkill", true);
        animator.SetTrigger("Seal");
    }

    override public void Activate()
    {
        MakePonds();
    }

    void MakePonds()
    {
        for (int i = 1; i <= pondCount; i++)
        {
            GameObject initiatedPond = Instantiate(SealPond, transform.position, transform.rotation);
            initiatedPond.transform.position = GetRandomPointInCircle(transform.position, CreationRadius);
            SealPond pondDamage = SealPond.GetComponent<SealPond>();
            pondDamage.SetUp(OwnerAbilityStatus, damageScale);
            pondDamage.SetUp(duration, silenceTime);
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
