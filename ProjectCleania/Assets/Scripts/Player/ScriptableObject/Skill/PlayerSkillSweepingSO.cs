using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillSweeping", menuName = "Scriptable Object/PlayerSkill/PlayerSkillSweeping")]
public class PlayerSkillSweepingSO : PlayerSkillSO
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

        string stunTime = StunTime.ToString();
        tempString = tempString.Replace("StunTime", stunTime);

        string sweepRange = SweepRange.ToString();
        tempString = tempString.Replace("SweepRange", sweepRange);

        return tempString;
    }

    [Header("경직 시간")]
    public float StunTime = 2;
    public float GetStunTime() { return StunTime; }

    [Header("쓸어담기 반경")]
    public float SweepRange = 2f;
    public float GetSweepRange() { return SweepRange; }
}
