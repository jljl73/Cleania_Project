using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityToxicity", menuName = "Scriptable Object/Enemy/SpecialAbilityToxicity")]
public class SpecialAbilityToxicitySO : PondSO
{
    [Header("���� �� �Ÿ�")]
    public float DistanceInterval;
    public float GetDistanceInterval() { return DistanceInterval; }

    [Header("���� ���� �ð� ����")]
    public float GenerationTimeInterval;
    public float GetGenerationTimeInterval() { return GenerationTimeInterval; }
}
