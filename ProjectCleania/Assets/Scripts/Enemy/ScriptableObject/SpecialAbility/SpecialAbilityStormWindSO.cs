using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityStormWind", menuName = "Scriptable Object/Enemy/SpecialAbilityStormWind")]
public class SpecialAbilityStormWindSO : EnemySkillSO
{
    [Header("���� �ð�")]
    public float Duration;
    public float GetDuration() { return Duration; }

    [Header("�˵��� �Ÿ�")]
    public float OrbitOffset;
    public float GetOrbitOffset() { return OrbitOffset; }

    [Header("�˵� ����")]
    public int Count;
    public int GetCount() { return Count; }

    [Header("�˵� �� ��ü �ӵ�")]
    public float Speed;
    public float GetSpeed() { return Speed; }
}
