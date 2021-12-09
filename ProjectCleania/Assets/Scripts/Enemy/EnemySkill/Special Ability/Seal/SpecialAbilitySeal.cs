using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SpecialAbilitySeal : EnemySkill
{
    [SerializeField]
    SpecialAbilitySealSO skillData;
    
    float damageScale = 10;
    float duration;
    float radius;
    float CreationRadius;           // 생성 반경
    float pondCount;
    float silenceTime;
    // 침묵 상태 이상

    [SerializeField]
    GameObject sealPond;

    SphereCollider triggerCollider;


    public override bool IsPassiveSkill { get { return skillData.GetIsPassiveSkill(); } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        triggerCollider = GetComponent<SphereCollider>();
        //if (SealPond == null)
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
        duration = skillData.GetDuration();
        radius = skillData.GetRadius();
        CreationRadius = skillData.GetCreationRadius();
        pondCount = skillData.GetCount();
        silenceTime = skillData.GetSilenceTime();
    }

    public override bool AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSpecialSkill", true);
        animator.SetTrigger("Seal");

        return true;
    }

    override public void Activate()
    {
        MakePonds();
    }

    void MakePonds()
    {
        for (int i = 1; i <= pondCount; i++)
        {
            // SealPond sealPond = ObjectPool.SpawnFromPool<SealPond>(ObjectPool.enumPoolObject.Seal, GetRandomPointInCircle(transform.position, CreationRadius), transform.rotation);
            GameObject initiatedPond = Instantiate(sealPond, transform.position, transform.rotation);
            sealPond.gameObject.transform.position = GetRandomPointInCircle(transform.position, CreationRadius);
            SealPond pondDamage = initiatedPond.GetComponent<SealPond>();
            pondDamage.SetUp(OwnerAbilityStatus, damageScale);
            pondDamage.SetUp(duration, silenceTime, radius);
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
        base.Deactivate();
        animator.SetBool("OnSpecialSkill", false);
        animator.SetBool("OnSkill", false);
    }
}
