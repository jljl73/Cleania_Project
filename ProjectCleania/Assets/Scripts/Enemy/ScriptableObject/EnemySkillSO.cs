using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySkill", menuName = "Scriptable Object/Enemy/EnemySkill")]
public class EnemySkillSO : EnemySkillIDSO
{
    [SerializeField]
    string SkillName;
    public string GetSkillName() { return SkillName; }

    [Header("Tip: 변수명을 입력할 수 있습니다.")]
    [TextArea]
    [SerializeField]
    string SkillDetails;

    public string GetSkillDetails()
    {
        string tempString = SkillDetails;

        string coolTime = CoolTime.ToString();
        tempString = tempString.Replace("CoolTime", coolTime);

        string createdMP = CreatedMP.ToString();
        tempString = tempString.Replace("CreatedMP", createdMP);

        string consumMP = ConsumMP.ToString();
        tempString = tempString.Replace("ConsumMP", consumMP);

        return tempString;
    }

    [Header("패시브 스킬")]
    [SerializeField]
    bool IsPassiveSkill = false;  // 추후 private 처리
    public bool GetIsPassiveSkill() { return IsPassiveSkill; }

    [Header("쿨타임")]
    [SerializeField]
    float CoolTime;  // 추후 private 처리
    public float GetCoolTime() { return CoolTime; }

    [Header("생성 고유 자원")]
    [SerializeField]
    float CreatedMP = 0f;
    public float GetCreatedMP() { return CreatedMP; }

    [Header("소모 고유 자원")]
    [SerializeField]
    float ConsumMP = 0f;
    public float GetConsumMP() { return ConsumMP; }

    [Header("전체 애니메이션 배속")]
    [SerializeField]
    float SpeedMultiplier = 1.0f;
    public float GetSpeedMultiplier() { return SpeedMultiplier; }

    [Header("발동 가능 위치")]
    [SerializeField]
    Vector3 triggerPosition = Vector3.zero;
    public Vector3 GetTriggerPosition() { return triggerPosition; }

    [Header("발동 가능 범위")]
    [SerializeField]
    float triggerRange = 1f;
    public float GetTriggerRange() { return triggerRange; }

    [Header("피해율")]
    [SerializeField]
    float DamageRate;
    public float GetDamageRate() { return DamageRate; }
}
