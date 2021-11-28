using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Suicide", menuName = "Scriptable Object/Enemy/DustySuicideSkill")]
public class DustySuicideSO : EnemySkillSO
{
    [Header("������ ����")]
    [SerializeField]
    float damageRange = 1f;
    public float GetDamageRange() { return damageRange; }

    [Header("�ߵ� Ȯ��")]
    [SerializeField]
    float triggerChance = 0.5f;
    public float GetTriggerChance() { return triggerChance; }

    [Header("�ߵ� ü�� ex) 0.1 = ü�� 10%�� �ߵ�")]
    [SerializeField]
    float triggerHPRate = 0.1f;
    public float GetTriggerHPRate() { return triggerHPRate; }

    [Header("�г� �ð�")]
    public float AngryDuration;
    public float GetAngryDuration() { return AngryDuration; }
}
