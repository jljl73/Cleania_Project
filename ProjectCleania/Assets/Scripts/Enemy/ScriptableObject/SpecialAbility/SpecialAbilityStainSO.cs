using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityStain", menuName = "Scriptable Object/Enemy/SpecialAbilityStain")]
public class SpecialAbilityStainSO : EnemySkillSO
{
    [Header("����ü ũ��")]
    public float StainRadius;
    public float GetStainRadius() { return StainRadius; }

    [Header("���� �ݰ�")]
    public float CreationRadius;
    public float GetCreationRadius() { return CreationRadius; }

    [Header("���� ����")]
    public int Count;
    public int GetCount() { return Count; }

    [Header("����ü ���� �ð�")]
    public float StopTime;
    public float GetStopTime() { return StopTime; }

    [Header("����~���� �ð�")]
    public float ProjFlightTime;
    public float GetProjFlightTime() { return ProjFlightTime; }

    [Header("�ı��� �� ���� ����")]
    public float DestroyAttackRange;
    public float GetDestroyAttackRange() { return DestroyAttackRange; }

    [Header("�ı��� �� ������")]
    public float DestroyAttackScale;
    public float GetDestroyAttackScale() { return DestroyAttackScale; }
}
