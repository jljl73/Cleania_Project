using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityToxicity : EnemySkill
{
    float damageScale = 10;
    float duration;
    float radius;
    float distanceInterval;
    float generationTimeInterval;
    float pondCount;
    
    //public GameObject DustPond;

    [SerializeField]
    SpecialAbilityToxicitySO skillData;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        //if (DustPond == null)
        //    throw new System.Exception("SpecialAbilityToxicity doesnt have DustPond");
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
        distanceInterval = skillData.GetDistanceInterval();
        generationTimeInterval = skillData.GetGenerationTimeInterval();
        pondCount = skillData.GetCount();
    }

    public override bool AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSpecialSkill", true);
        animator.SetTrigger("Toxicity");
        return true;
    }

    override public void Activate()
    {
        StartCoroutine(MakePonds());
    }
    
    IEnumerator MakePonds()
    {
        Vector3 tempUp = transform.up;
        Vector3 tempForward = transform.forward;
        Vector3 tempPosition = transform.position;
        Quaternion tempQuaternion = transform.rotation;
        for (int i = 1; i <= pondCount; i++)
        {
            //GameObject initiatedPond = Instantiate(DustPond, tempPosition, tempQuaternion);
            Vector3 spawnedPos = tempPosition + (tempForward * distanceInterval * i + tempUp * 0.2f);
            ToxicityPond toxicityPond = ObjectPool.SpawnFromPool<ToxicityPond>(ObjectPool.enumPoolObject.Toxicity, spawnedPos, tempQuaternion);
            //initiatedPond.transform.position += (initiatedPond.transform.forward * distanceInterval * i + initiatedPond.transform.up * 0.2f);
            if (enemy.abilityStatus == null)
                print("enemy.abilityStatus is null");
            else
                print("enemy.abilityStatus not null");
            toxicityPond.SetUp(duration ,OwnerAbilityStatus, damageScale, radius);

            //Destroy(initiatedPond, duration);

            yield return new WaitForSeconds(generationTimeInterval);
        }
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSpecialSkill", false);
        animator.SetBool("OnSkill", false);
    }
}
