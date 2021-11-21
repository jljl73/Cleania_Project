using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillRefreshingLeapForward", menuName = "Scriptable Object/PlayerSkill/PlayerSkillRefreshingLeapForward")]
public class PlayerSkillRefreshingLeapForwardSO : PlayerSkillSO
{
    public override string GetSkillDetails()
    {
        string tempString = SkillDetails;

        string coolTime = CoolTime.ToString();
        tempString = tempString.Replace("CoolTime", coolTime);

        string createdMP = CreatedMP.ToString();
        tempString = tempString.Replace("CreatedMP", createdMP);

        string consumMP = ConsumMP.ToString();
        tempString = tempString.Replace("ConsumMP", consumMP);

        string smashDamageRate = (SmashDamageRate * 100).ToString();
        tempString = tempString.Replace("SmashDamageRate", smashDamageRate);

        string smashRange = SmashRange.ToString();
        tempString = tempString.Replace("SmashRange", smashRange);

        string stunTime = StunTime.ToString();
        tempString = tempString.Replace("StunTime", stunTime);

        string slowTime = SlowTime.ToString();
        tempString = tempString.Replace("SlowTime", slowTime);

        return tempString;
    }

    [Header("도약 이팩트 반경")]
    public float JumpEffectSize = 1f;
    public float GetJumpEffectSize() { return JumpEffectSize; }

    [Header("빗자루 휘두름 반경")]
    public float SwingDownSize = 1f;
    public float GetSwingDownSize() { return SwingDownSize; }

    [Header("내려치기 데미지 비율 (ex. 2.0 = 200% 데미지 적용)")]
    public float SmashDamageRate = 5.4f;
    public float GetSmashDamageRate() { return SmashDamageRate; }

    [Header("땅 내려찍기 반경")]
    public float SmashRange = 2f;
    public float GetSmashRange() { return SmashRange; }

    [Header("경직 시간")]
    public float StunTime = 1.5f;
    public float GetStunTime() { return StunTime; }

    [Header("슬로우 시간")]
    public float SlowTime = 2f;
    public float GetSlowTime() { return SlowTime; }

    [Header("제자리 점프 거리")]
    public float JumpDistance = 7f;
    public float GetJumpDistance() { return JumpDistance; }

    [Header("애니메이션 분리 갯수")]
    public int AnimationSplitCount = 1;
    public int GetAnimationSplitCount() { return AnimationSplitCount; }


    /*
     반원의 형태를 그리며 전방으로 뛰어오른 후 무기로 찍으며 착지하며, 540%의 피해를 줍니다. 피격된 적들은 1.5초간 경직 상태에 빠진 뒤, 2초간 슬로우 상태가 됩니다.
     */
}
