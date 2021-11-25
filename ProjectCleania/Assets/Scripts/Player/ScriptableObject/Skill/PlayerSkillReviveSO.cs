using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillRevive", menuName = "Scriptable Object/PlayerSkill/PlayerSkillRevive")]
public class PlayerSkillReviveSO : PlayerSkillSO
{
    public override string GetSkillDetails()
    {
        string tempString = SkillDetails;

        //string coolTime = CoolTime.ToString();
        //tempString = tempString.Replace("CoolTime", coolTime);



        return tempString;
    }

    [Header("소모 경험치율")]
    [SerializeField]
    protected float consumedXPRate = 0.05f;
    public float GetConsumedXPRate() { return consumedXPRate; }

    [Header("소모 내구도율")]
    [SerializeField]
    protected float consumedDurabilityRate = 0.1f;
    public float GetConsumedDurabilityRate() { return consumedDurabilityRate; }

    [Header("소모 클린")]
    [SerializeField]
    protected int consumedClean = 1000;
    public int GetConsumedClean() { return consumedClean; }
}
