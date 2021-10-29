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


    public GameObject DustPond;

    [SerializeField]
    SpecialAbilityToxicitySO skillData;

    private new void Awake()
    {
        base.Awake();
        if (DustPond == null)
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

        SkillName = skillData.GetSkillName();
        SkillDetails = skillData.GetSkillDetails();
        CoolTime = skillData.GetCoolTime();
        CreatedMP = skillData.GetCreatedMP();
        ConsumMP = skillData.GetConsumMP();
        SpeedMultiplier = skillData.GetSpeedMultiplier();

        duration = skillData.GetDuration();
        radius = skillData.GetRadius();
        distanceInterval = skillData.GetDistanceInterval();
        generationTimeInterval = skillData.GetGenerationTimeInterval();
        pondCount = skillData.GetCount();
    }

    public override void AnimationActivate()
    {
        animator.SetBool("OnSkill", true);
        animator.SetBool("OnSpecialSkill", true);
        animator.SetTrigger("Toxicity");
    }

    override public void Activate()
    {
        StartCoroutine(MakePonds());
    }
    
    IEnumerator MakePonds()
    {
        for (int i = 1; i <= pondCount; i++)
        {
            GameObject initiatedPond = Instantiate(DustPond, transform.position, transform.rotation);
            initiatedPond.transform.position += (initiatedPond.transform.forward * distanceInterval * i + initiatedPond.transform.up * 0.2f);
            PondDamage pondDamage = DustPond.GetComponent<PondDamage>();
            if (pondDamage != null)
            {
                print("Pond not null");
                if (enemy.abilityStatus == null)
                    print("enemy.abilityStatus is null");
                else
                    print("enemy.abilityStatus not null");
                pondDamage.SetProperty(enemy.abilityStatus, damageScale);
            }
            else
                print("Pond null");

            Destroy(initiatedPond, duration);

            yield return new WaitForSeconds(generationTimeInterval);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Player>().abilityStatus.AttackedBy(enemy.abilityStatus, damageScale);
        }
    }

    public override void Deactivate()
    {
        animator.SetBool("OnSpecialSkill", false);
        animator.SetBool("OnSkill", false);
    }


}
