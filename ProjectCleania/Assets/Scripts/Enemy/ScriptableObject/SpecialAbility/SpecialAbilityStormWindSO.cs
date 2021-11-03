using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityStormWind", menuName = "Scriptable Object/Enemy/SpecialAbilityStormWind")]
public class SpecialAbilityStormWindSO : EnemySkillSO
{
    [Header("Áö¼Ó ½Ã°£")]
    public float Duration;
    public float GetDuration() { return Duration; }

    [Header("±Ëµµ°£ °Å¸®")]
    public float OrbitOffset;
    public float GetOrbitOffset() { return OrbitOffset; }

    [Header("±Ëµµ °¹¼ö")]
    public int Count;
    public int GetCount() { return Count; }

    [Header("±Ëµµ ³» ¹°Ã¼ ¼Óµµ")]
    public float Speed;
    public float GetSpeed() { return Speed; }
}
