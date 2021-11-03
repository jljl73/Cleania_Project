using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityIngrainedDirt", menuName = "Scriptable Object/Enemy/SpecialAbilityIngrainedDirt")]
public class SpecialAbilityIngrainedDirtSO : EnemySkillSO
{
    [Header("����� ������")]
    public float HPIncreaseRate;
    public float GetHPIncreaseRate() { return HPIncreaseRate; }
}
