using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillSweeping", menuName = "Scriptable Object/PlayerSkill/PlayerSkillSweeping")]
public class PlayerSkillSweepingSO : ScriptableObject
{
    public string SkillName;
    [Header("Tip: 변수명을 입력할 수 있습니다.")]
    [TextArea]
    public string SkillDetails;
    public string GetSkillDetails()
    {
        string tempString = SkillDetails;

        string coolTime = CoolTime.ToString();
        tempString = tempString.Replace("CoolTime", coolTime);

        string createdMP = CreatedMP.ToString();
        tempString = tempString.Replace("CreatedMP", createdMP);

        string consumMP = ConsumMP.ToString();
        tempString = tempString.Replace("ConsumMP", consumMP);

        string stunTime = StunTime.ToString();
        tempString = tempString.Replace("StunTime", stunTime);

        string sweepRange = SweepRange.ToString();
        tempString = tempString.Replace("SweepRange", sweepRange);

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

    [Header("경직 시간")]
    public float StunTime = 2;

    [Header("쓸어담기 범위")]
    public float SweepRange = 2f;
}
