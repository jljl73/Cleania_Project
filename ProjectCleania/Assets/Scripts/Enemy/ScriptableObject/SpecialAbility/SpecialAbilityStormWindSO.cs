using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityStormWind", menuName = "Scriptable Object/Enemy/SpecialAbilityStormWind")]
public class SpecialAbilityStormWindSO : EnemySkillSO
{
    [Header("지속 시간")]
    public float Duration;
    public float GetDuration() { return Duration; }

    [Header("궤도간 거리")]
    public float OrbitOffset;
    public float GetOrbitOffset() { return OrbitOffset; }

    [Header("궤도 갯수")]
    public int Count;
    public int GetCount() { return Count; }

    [Header("궤도 내 물체 속도")]
    public float Speed;
    public float GetSpeed() { return Speed; }

    [Header("물체 크기")]
    public float DamageSize;
    public float GetDamageSize() { return DamageSize; }
}
