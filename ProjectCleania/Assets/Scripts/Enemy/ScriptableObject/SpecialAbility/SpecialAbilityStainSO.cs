using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityStain", menuName = "Scriptable Object/Enemy/SpecialAbilityStain")]
public class SpecialAbilityStainSO : EnemySkillSO
{
    [Header("투사체 크기")]
    public float StainRadius;
    public float GetStainRadius() { return StainRadius; }

    [Header("생성 반경")]
    public float CreationRadius;
    public float GetCreationRadius() { return CreationRadius; }

    [Header("생성 갯수")]
    public int Count;
    public int GetCount() { return Count; }

    [Header("투사체 멈춤 시간")]
    public float StopTime;
    public float GetStopTime() { return StopTime; }

    [Header("시작~도착 시간")]
    public float ProjFlightTime;
    public float GetProjFlightTime() { return ProjFlightTime; }

    [Header("파괴될 때 공격 범위")]
    public float DestroyAttackRange;
    public float GetDestroyAttackRange() { return DestroyAttackRange; }

    [Header("파괴될 때 피해율")]
    public float DestroyAttackScale;
    public float GetDestroyAttackScale() { return DestroyAttackScale; }
}
