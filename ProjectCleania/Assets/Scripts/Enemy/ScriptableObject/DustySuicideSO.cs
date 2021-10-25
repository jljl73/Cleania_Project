using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Suicide", menuName = "Scriptable Object/Enemy/DustySuicideSkill")]
public class DustySuicideSO : EnemySkillSO
{
    [Header("�ߵ� ü�� ex) 0.1 = ü�� 10%�� �ߵ�")]
    public float TriggerChance = 0.1f;
    public float GetTriggerChance() { return TriggerChance; }

    [Header("�г� �ð�")]
    public float AngryDuration;
    public float GetAngryDuration() { return AngryDuration; }
}
