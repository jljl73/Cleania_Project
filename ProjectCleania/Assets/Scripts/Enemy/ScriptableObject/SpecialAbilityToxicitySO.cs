using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityToxicity", menuName = "Scriptable Object/Enemy/SpecialAbilityToxicity")]
public class SpecialAbilityToxicitySO : EnemySkillSO
{
    [Header("지속 시간")]
    public float Duration;
    public float GetDuration() { return Duration; }

    [Header("장판 반경 (미적용)")]
    public float Radius;
    public float GetRadius() { return Radius; }

    [Header("장판 간 거리")]
    public float DistanceInterval;
    public float GetDistanceInterval() { return DistanceInterval; }

    [Header("장판 생성 시간 간격")]
    public float GenerationTimeInterval;
    public float GetGenerationTimeInterval() { return GenerationTimeInterval; }

    [Header("장판 갯수")]
    public float Count;
    public float GetCount() { return Count; }
}
