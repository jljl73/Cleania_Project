using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GroundHit", menuName = "Scriptable Object/Enemy/GroundHit")]
public class GroundHitSO : EnemySkillSO
{
    [Header("피해 범위")]
    [SerializeField]
    float damageRadius;
    public float GetDamageRadius() { return damageRadius; }

    [Header("내려침 위치 거리")]
    [SerializeField]
    float attackPoseFromTrigger = 2f;
    public float GetAttackPoseFromTrigger() { return attackPoseFromTrigger; }

    [Header("스턴 시간")]
    [SerializeField]
    float stunnedTime;
    public float GetStunnedTime() { return stunnedTime; }

    [Header("충격파 피해율")]
    [SerializeField]
    float indirectDamageRate;
    public float GetIndirectDamageRate() { return indirectDamageRate; }

    [Header("충격파 피해 범위")]
    [SerializeField]
    float indirectDamageRadius;
    public float GetIndirectDamageRadius() { return indirectDamageRadius; }

    [Header("발동 확률")]
    [SerializeField]
    float triggerProbability;
    public float GetTriggerProbability() { return triggerProbability; }
}
