using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Suicide", menuName = "Scriptable Object/Enemy/DustySuicideSkill")]
public class DustySuicideSO : EnemySkillSO
{
    [Header("발동 체력 ex) 0.1 = 체력 10%에 발동")]
    public float TriggerChance = 0.1f;
    public float GetTriggerChance() { return TriggerChance; }

    [Header("분노 시간")]
    public float AngryDuration;
    public float GetAngryDuration() { return AngryDuration; }
}
