using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialAbilityPollution : EnemySkill
{
    float pollutionDuration = 3;
    float damageRate;

    bool abilityActivate = false;

    [SerializeField]
    GameObject pollutionPrefab;

    [SerializeField]
    SpecialAbilityPollutionSO skillData;

    public override bool IsPassiveSkill { get { return skillData.IsPassiveSkill; } }
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

    private void FixedUpdate()
    {
        if (!abilityActivate) return;
        GameObject obj = Instantiate(pollutionPrefab, transform.position, transform.rotation);
        ContactStayDamage contactStayDamage = obj.GetComponent<ContactStayDamage>();
        contactStayDamage.SetUp(OwnerAbilityStatus, damageRate);

        Destroy(obj, pollutionDuration);
    }

    public void UpdateSkillData()
    {
        if (skillData == null)
            throw new System.Exception("SpecialAbilityIngrainedDirt no skillData");

        base.UpdateSkillData(skillData);

        damageRate = skillData.GetDamageRate();
        pollutionDuration = skillData.GetDuration();
    }

    public override void AnimationActivate()
    {
        UpdateSkillData();

        abilityActivate = true;
        // 체력 증가
        //enemy.buff
    }

    public override void Deactivate()
    {
        return;
    }
}
