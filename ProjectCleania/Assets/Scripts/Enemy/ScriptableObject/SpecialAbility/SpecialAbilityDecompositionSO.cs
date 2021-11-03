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

    [Header("���� �ð�")]
    public float ExistTime;
    public float GetExistTime() { return ExistTime; }

    [Header("���� �ݰ�")]
    public float CreationRadius;
    public float GetCreationRadius() { return CreationRadius; }

    [Header("��� �ӵ�")]
    public float Speed;
    public float GetSpeed() { return Speed; }

    [Header("���� ���� �ð�")]
    public float ExplodeWaitTime;
    public float GetExplodeWaitTime() { return ExplodeWaitTime; }

    [Header("���� ������ �ǰ� �Ÿ�")]
    public float ExplodeDamageRange;
    public float GetExplodeDamageRange() { return ExplodeDamageRange; }

    [Header("���� �ð�")]
    public float StunTime;
    public float GetStunTime() { return StunTime; }
}
