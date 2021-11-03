using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityIngrainedDirt", menuName = "Scriptable Object/Enemy/SpecialAbilityIngrainedDirt")]
public class SpecialAbilityIngrainedDirtSO : EnemySkillSO
{
    [Header("생명력 증가율")]
    public float HPIncreaseRate;
    public float GetHPIncreaseRate() { return HPIncreaseRate; }
}
