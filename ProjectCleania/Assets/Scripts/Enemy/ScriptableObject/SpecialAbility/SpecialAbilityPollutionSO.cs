using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityPollution", menuName = "Scriptable Object/Enemy/SpecialAbilityPollution")]
public class SpecialAbilityPollutionSO : EnemySkillSO
{
    [Header("������ ����")]
    public float DamageRange = 1f;
    public float GetDamageRange() { return DamageRange; }

    [Header("���� ���� �ð�")]
    public float Duration;
    public float GetDuration() { return Duration; }

    //[Header("���� ���� ��� �ð�")]
    //public float PreparationTime;
    //public float GetPreparationTime() { return PreparationTime; }
}
