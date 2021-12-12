using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerSkillVillageReturn : PlayerSkill
{
    [SerializeField]
    PlayerSkillVillageReturnSO skillData;

    float timeCost;
    string villageName;
    Vector3 returnPosition = Vector3.zero;

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

        timeCost = skillData.GetTimeCost();
        villageName = skillData.GetVillageName();
        returnPosition = skillData.GetReturnPosition();
    }

    public override bool AnimationActivate()
    {
        base.AnimationActivate();


        return true;
    }

    public override void Activate()
    {
        base.Activate();
        ReturnToVillage();
    }

    public void ReturnToVillage()
    {
        if (SceneManager.GetActiveScene().name != villageName)
            GameManager.Instance.ChangeScene(villageName);
        PlayerController player = FindObjectOfType<PlayerController>();
        player.gameObject.transform.position = returnPosition;
    }
}
