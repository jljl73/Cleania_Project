using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityFastFeet", menuName = "Scriptable Object/Enemy/SpecialAbilityFastFeet")]
public class SpecialAbilityFastFeetSO : EnemySkillSO
{
    [Header("���� & �̼� ������")]
    public float SpeedIncreaseRate;
    public float GetSpeedIncreaseRate() { return SpeedIncreaseRate; }
}
