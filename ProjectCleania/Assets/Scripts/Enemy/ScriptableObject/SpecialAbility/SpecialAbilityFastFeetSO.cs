using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityFastFeet", menuName = "Scriptable Object/Enemy/SpecialAbilityFastFeet")]
public class SpecialAbilityFastFeetSO : EnemySkillSO
{
    [Header("공속 & 이속 증가율")]
    public float SpeedIncreaseRate;
    public float GetSpeedIncreaseRate() { return SpeedIncreaseRate; }
}
