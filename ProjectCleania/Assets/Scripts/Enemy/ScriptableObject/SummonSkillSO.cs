using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SummonSkill", menuName = "Scriptable Object/Enemy/SummonSkill")]
public class SummonSkillSO : EnemySkillSO
{
    //public string SkillName;
    //public string GetSkillName() { return SkillName; }

    //[Header("Tip: 변수명을 입력할 수 있습니다.")]
    //[TextArea]
    //public string SkillDetails;

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

    //// public bool isAttacking;
    //[Header("쿨타임")]
    //public float CoolTime;  // 추후 private 처리
    //public float GetCoolTime() { return CoolTime; }

    //[Header("생성 고유 자원")]
    //public float CreatedMP = 0f;
    //public float GetCreatedMP() { return CreatedMP; }

    //[Header("소모 고유 자원")]
    //public float ConsumMP = 0f;
    //public float GetConsumMP() { return ConsumMP; }

    //[Header("전체 애니메이션 배속")]
    //public float SpeedMultiplier = 1.0f;
    //public float GetSpeedMultiplier() { return SpeedMultiplier; }

    [Header("소환 몬스터")]
    public GameObject HighDusty;
    public GameObject GetHighDustyForSummon() { return HighDusty; }
    public GameObject normalDusty;
    public GameObject GetNormalDustyForSummon() { return normalDusty; }
}
