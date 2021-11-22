using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GroundHit", menuName = "Scriptable Object/Enemy/GroundHit")]
public class GroundHitSO : EnemySkillSO
{
    [Header("���� ����")]
    [SerializeField]
    float damageRadius;
    public float GetDamageRadius() { return damageRadius; }

    [Header("���� �ð�")]
    [SerializeField]
    float stunnedTime;
    public float GetStunnedTime() { return stunnedTime; }

    [Header("����� ������")]
    [SerializeField]
    float indirectDamageRate;
    public float GetIndirectDamageRate() { return indirectDamageRate; }

    [Header("����� ���� ����")]
    [SerializeField]
    float indirectDamageRadius;
    public float GetIndirectDamageRadius() { return indirectDamageRadius; }

    [Header("�ߵ� Ȯ��")]
    [SerializeField]
    float triggerProbability;
    public float GetTriggerProbability() { return triggerProbability; }
}
