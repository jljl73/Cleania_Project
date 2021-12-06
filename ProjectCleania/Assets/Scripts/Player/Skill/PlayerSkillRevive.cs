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
    }

    protected new void Start()
    {
        base.Start();
        UpdateSkillData();
    }

    public void UpdateSkillData()
    {
        base.UpdateSkillData(skillData);
    }

}
