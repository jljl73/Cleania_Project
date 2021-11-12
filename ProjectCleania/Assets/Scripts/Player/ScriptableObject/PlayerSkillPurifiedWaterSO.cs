using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillPurifiedWater", menuName = "Scriptable Object/PlayerSkill/PlayerSkillPurifiedWater")]
public class PlayerSkillPurifiedWaterSO : PlayerSKillIDSO
{
    public string SkillName;
    public string GetSkillName() { return SkillName; }

    [Header("Tip: 변수명을 입력할 수 있습니다.")]
    [TextArea]
    public string SkillDetails;
    public string GetSkillDetails()
    {
        string tempString = SkillDetails;

        string coolTime = CoolTime.ToString();
        tempString = tempString.Replace("CoolTime", coolTime);

        return tempString;
    }

    [Header("작동 키")]
    public KeyCode TriggerKey;
    public KeyCode GetTriggerKey() { return TriggerKey; }

    // public bool isAttacking;
    [Header("쿨타임")]
    public float CoolTime = 25f;  // 추후 private 처리
    public float GetCoolTime() { return CoolTime; }

    [Header("소모 고유 자원")]
    public float ConsumMP = 0f;
    public float GetConsumMP() { return ConsumMP; }

    [Header("전체 애니메이션 배속")]
    public float SpeedMultiplier = 1.0f;
    public float GetSpeedMultiplier() { return SpeedMultiplier; }

    [Header("체력 회복율")]
    public float HPRecoveryRate = 0.6f;
    public float GetHPRecoveryRate() { return HPRecoveryRate; }

    [Header("코스트 회복률")]
    public float MPRecoveryRate = 0.3f;
    public float GetMPRecoveryRate() { return MPRecoveryRate; }
}
