using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillRevive : PlayerSkill
{
    [SerializeField]
    PlayerSkillReviveSO skillData;

    float timeCost;
    string villageName;
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
    }

    public override bool AnimationActivate()
    {
        base.AnimationActivate();

        //animator.SetTrigger("Revive");
        //print("Revive skill AnimationActivate");
        return true;
    }

    public override void Activate()
    {
    }

    public override void Deactivate()
    {
        base.Deactivate();
    }

    //public override void 
}
