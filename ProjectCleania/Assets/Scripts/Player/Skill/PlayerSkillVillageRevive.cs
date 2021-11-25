using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillVillageRevive : PlayerSkill
{
    [SerializeField]
    PlayerSkillReviveSO skillData;
    [SerializeField]
    PlayerSkillVillageReturnSO referenceSkillData;

    float timeCost;
    float consumedXPRate = 0;
    float consumedDurabilityRate = 0;
    int consumedClean = 0;

   string villageName = "MY_DirtyForest_Village_1";
   Vector3 returnPosition;

    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
        UpdateSkillData();
    }

    protected new void Start()
    {
        base.Start();
    }

    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);
        consumedXPRate = skillData.GetConsumedXPRate();
        consumedDurabilityRate = skillData.GetConsumedDurabilityRate();
        consumedClean = skillData.GetConsumedClean();

        villageName = referenceSkillData.GetVillageName();
        returnPosition = referenceSkillData.GetReturnPosition();
    }

    public override bool AnimationActivate()
    {
        base.AnimationActivate();

        animator.SetTrigger("Revive");
        return true;
    }

    public override void Activate()
    {
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }
}
