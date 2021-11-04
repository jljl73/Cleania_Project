using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityToxicity", menuName = "Scriptable Object/Enemy/SpecialAbilityToxicity")]
public class SpecialAbilityToxicitySO : PondSO
{
    [Header("장판 간 거리")]
    public float DistanceInterval;
    public float GetDistanceInterval() { return DistanceInterval; }

    [Header("장판 생성 시간 간격")]
    public float GenerationTimeInterval;
    public float GetGenerationTimeInterval() { return GenerationTimeInterval; }
}
