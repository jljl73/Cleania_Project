using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityToxicity", menuName = "Scriptable Object/Enemy/SpecialAbilityToxicity")]
public class SpecialAbilityToxicitySO : EnemySkillSO
{
    [Header("���� �ð�")]
    public float Duration;
    public float GetDuration() { return Duration; }

    [Header("���� �ݰ� (������)")]
    public float Radius;
    public float GetRadius() { return Radius; }

    [Header("���� �� �Ÿ�")]
    public float DistanceInterval;
    public float GetDistanceInterval() { return DistanceInterval; }

    [Header("���� ���� �ð� ����")]
    public float GenerationTimeInterval;
    public float GetGenerationTimeInterval() { return GenerationTimeInterval; }

    [Header("���� ����")]
    public float Count;
    public float GetCount() { return Count; }
}
