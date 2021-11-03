using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityPollution", menuName = "Scriptable Object/Enemy/SpecialAbilityPollution")]
public class SpecialAbilityPollutionSO : EnemySkillSO
{
    [Header("오염 유지 시간")]
    public float Duration;
    public float GetDuration() { return Duration; }

    //[Header("지뢰 시전 대기 시간")]
    //public float PreparationTime;
    //public float GetPreparationTime() { return PreparationTime; }
}
