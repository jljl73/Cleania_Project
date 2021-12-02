using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillFairysWings", menuName = "Scriptable Object/PlayerSkill/PlayerSkillFairysWings")]
public class PlayerSkillFairysWingsSO : PlayerSkillSO
{
    [Header("지속 시간")]
    public float Duration = 5f;
    public float GetDuration() { return Duration; }

    [Header("속도 상승률 (ex. 0.4 = 40% 증가)")]
    public float SpeedUpRate = 1.4f;
    public float GetSpeedUpRate() { return SpeedUpRate; }
}
