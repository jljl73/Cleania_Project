using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillKatharsis", menuName = "Scriptable Object/PlayerSkill/PlayerSkillKatharsis")]
public class PlayerSkillKatharsisSO : PlayerSKillIDSO
{
    public string SkillName;
    public string GetSkillName() { return SkillName; }

    [Header("Tip: 변수명을 입력할 수 있습니다.")]
    [TextArea]
    public string SkillDetails;

    public string GetSkillDetails()
    {
        string tempString = SkillDetails;

        string triggerAvailablePercent = TriggerAvailablePercent.ToString();
        tempString = tempString.Replace("TriggerAvailablePercent", triggerAvailablePercent);

        

        return tempString;
    }

    [Header("작동 키")]
    public KeyCode TriggerKey;
    public KeyCode GetTriggerKey() { return TriggerKey; }

    // public bool isAttacking;
    [Header("쿨타임")]
    public float CoolTime;  // 추후 private 처리
    public float GetCoolTime() { return CoolTime; }

    [Header("시전 가능 청량감%")]
    public float TriggerAvailablePercent = 0.5f;
    public float GetTriggerAvailablePercent() { return TriggerAvailablePercent; }

    [Header("초당 소모 고유 자원")]
    public float ConsumMPPerSec = 0f;
    public float GetConsumMPPerSec() { return ConsumMPPerSec; }

    [Header("전체 애니메이션 배속")]
    public float SpeedMultiplier = 1.0f;
    public float GetSpeedMultiplier() { return SpeedMultiplier; }

    [Header("공격 속도 상승률")]
    public float AttackSpeedUpRate = 1f;
    public float GetAttackSpeedUpRate() { return AttackSpeedUpRate; }

    [Header("이동 속도 상승률")]
    public float MovekSpeedUpRate = 1f;
    public float GetMoveSpeedUpRate() { return MovekSpeedUpRate; }

    [Header("저항력 상승률")]
    public float ResistanceIncreaseRate = 1f;
    public float GetResistanceIncreaseRate() { return ResistanceIncreaseRate; }

    [Header("공격력 상승률")]
    public float StrikingPowerIncreaseRate = 1.0f;
    public float GetStrikingPowerIncreaseRate() { return StrikingPowerIncreaseRate; }

    [Header("방어력 상승률")]
    public float DefenceIncreaseRate = 1.0f;
    public float GetDefenceIncreaseRate() { return DefenceIncreaseRate; }
}
