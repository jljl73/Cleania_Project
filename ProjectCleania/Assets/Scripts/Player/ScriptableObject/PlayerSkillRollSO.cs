using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillRoll", menuName = "Scriptable Object/PlayerSkill/PlayerSkillRoll")]
public class PlayerSkillRollSO : PlayerSKillIDSO
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
    public float CoolTime;  // 추후 private 처리
    public float GetCoolTime() { return CoolTime; }

    [Header("소모 고유 자원")]
    public float ConsumMP = 0f;
    public float GetConsumMP() { return ConsumMP; }

    [Header("전체 애니메이션 배속")]
    public float SpeedMultiplier = 1.0f;
    public float GetSpeedMultiplier() { return SpeedMultiplier; }

    [Header("회피 거리")]
    public float AvoidDistance = 4f;
    public float GetAvoidDistance() { return AvoidDistance; }

    [Header("속도 배속")]
    public float AvoidSpeedMultiplier = 2f;
    public float GetAvoidSpeedMultiplier() { return AvoidSpeedMultiplier; }
}
