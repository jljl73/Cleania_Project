using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Suicide", menuName = "Scriptable Object/Enemy/DustySuicideSkill")]
public class DustySuicideSO : EnemySkillSO
{
    [Header("데미지 범위")]
    [SerializeField]
    float damageRange = 1f;
    public float GetDamageRange() { return damageRange; }

    [Header("발동 확률")]
    [SerializeField]
    float triggerChance = 0.5f;
    public float GetTriggerChance() { return triggerChance; }

    [Header("발동 체력 ex) 0.1 = 체력 10%에 발동")]
    [SerializeField]
    float triggerHPRate = 0.1f;
    public float GetTriggerHPRate() { return triggerHPRate; }

    [Header("분노 시간")]
    public float AngryDuration;
    public float GetAngryDuration() { return AngryDuration; }
}
