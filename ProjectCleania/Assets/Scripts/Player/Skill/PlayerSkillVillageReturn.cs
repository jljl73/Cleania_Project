using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillVillageReturn : PlayerSkill
{
    [SerializeField]
    PlayerSkillVillageReturnSO skillData;

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

        timeCost = skillData.GetTimeCost();
        villageName = skillData.GetVillageName();
        returnPosition = skillData.GetReturnPosition();
    }

    public override bool AnimationActivate()
    {
        base.AnimationActivate();

        ReturnToVillage();

        return true;
    }

    public void ReturnToVillage()
    {
        GameManager.Instance.ChangeScene(villageName);
        Player player = FindObjectOfType<Player>();
        player.gameObject.transform.position = returnPosition;
    }
}
