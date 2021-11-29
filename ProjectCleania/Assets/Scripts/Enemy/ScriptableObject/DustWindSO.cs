using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DustWindSkill", menuName = "Scriptable Object/Enemy/DustWindSkill")]
public class DustWindSO : EnemySkillSO
{
    //public string GetSkillDetails()
    //{
    //    string tempString = SkillDetails;

    //    string coolTime = CoolTime.ToString();
    //    tempString = tempString.Replace("CoolTime", coolTime);

    //    string createdMP = CreatedMP.ToString();
    //    tempString = tempString.Replace("CreatedMP", createdMP);

    //    string consumMP = ConsumMP.ToString();
    //    tempString = tempString.Replace("ConsumMP", consumMP);

    //    return tempString;
    //}

    [Header("밀어치기 힘")]
    public float HitForce;
    public float GetHitForce() { return HitForce; }

    [Header("밀어치기 반경 (수정 금지)")]
    public float PushRadius;
    public float GetPushRadius() { return PushRadius; }
}
