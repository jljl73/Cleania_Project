using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillFairysWings", menuName = "Scriptable Object/PlayerSkill/PlayerSkillFairysWings")]
public class PlayerSkillFairysWingsSO : ScriptableObject
{
    public string SkillName;
    [Header("Tip: 변수명을 입력할 수 있습니다.")]
    [TextArea]
    public string SkillDetails;
    public string GetSkillDetails()
    {
        string tempString = SkillDetails;

        string duration = Duration.ToString();
        tempString = tempString.Replace("Duration", duration);

        string speedUpRate = (SpeedUpRate * 100).ToString();
        tempString = tempString.Replace("SpeedUpRate", speedUpRate);

        string coolTime = CoolTime.ToString();
        tempString = tempString.Replace("CoolTime", coolTime);

        string createdMP = CreatedMP.ToString();
        tempString = tempString.Replace("CreatedMP", createdMP);

        string consumMP = ConsumMP.ToString();
        tempString = tempString.Replace("ConsumMP", consumMP);

        return tempString;
    }

    [Header("작동 키")]
    public string TriggerKey;

    // public bool isAttacking;
    [Header("쿨타임")]
    public float CoolTime;  // 추후 private 처리
    public float GetCoolTime { get { return CoolTime; } }

    [Header("생성 고유 자원")]
    public float CreatedMP = 0f;

    [Header("소모 고유 자원")]
    public float ConsumMP = 0f;

    [Header("전체 애니메이션 배속")]
    public float speedMultiplier = 1.0f;

    [Header("지속 시간")]
    public float Duration = 5f;

    [Header("속도 상승률 (ex. 0.4 = 40% 증가)")]
    public float SpeedUpRate = 1.4f;

    
}
