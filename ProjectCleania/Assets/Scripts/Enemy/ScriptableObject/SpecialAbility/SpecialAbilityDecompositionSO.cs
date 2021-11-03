using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityDecomposition", menuName = "Scriptable Object/Enemy/SpecialAbilityDecomposition")]
public class SpecialAbilityDecompositionSO : EnemySkillSO
{
    float speed = 0.5f;
    float explodeWaitTime = 3f;
    float explodeDamageRange = 5f;
    float stunTime = 2f;

    [Header("지속 시간")]
    public float ExistTime;
    public float GetExistTime() { return ExistTime; }

    [Header("생성 반경")]
    public float CreationRadius;
    public float GetCreationRadius() { return CreationRadius; }

    [Header("얼룩 속도")]
    public float Speed;
    public float GetSpeed() { return Speed; }

    [Header("폭발 시전 시간")]
    public float ExplodeWaitTime;
    public float GetExplodeWaitTime() { return ExplodeWaitTime; }

    [Header("폭발 데미지 피격 거리")]
    public float ExplodeDamageRange;
    public float GetExplodeDamageRange() { return ExplodeDamageRange; }

    [Header("기절 시간")]
    public float StunTime;
    public float GetStunTime() { return StunTime; }
}
