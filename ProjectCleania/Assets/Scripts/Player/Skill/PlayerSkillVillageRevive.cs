using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillVillageRevive : PlayerSkill
{
    [SerializeField]
    PlayerSkillReviveSO skillData;
    [SerializeField]
    PlayerSkillVillageReturnSO referenceSkillData;

    [SerializeField]
    PlayerSkillVillageReturn playerSkillVillageReturn;

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

        if (playerSkillVillageReturn == null)
            throw new System.Exception("PlayerSkillVillageRevive doesnt have playerSkillVillageReturn!");

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

        //animator.SetTrigger("VillageRevive");

        playerSkillVillageReturn.ReturnToVillage();
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
