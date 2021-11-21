using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DustStorm", menuName = "Scriptable Object/Enemy/DustStorm")]
public class DustStormSO : EnemySkillSO
{
    [Header("��� ���� ����")]
    [SerializeField]
    float damageRadius = 1;
    public float GetDamageRadius() { return damageRadius; }

    [Header("������ �ӵ�")]
    [SerializeField]
    float pulledSpeed = 5;
    public float GetPulledSpeed() { return pulledSpeed; }

    [Header("��ǳ ���� �ð�")]
    [SerializeField]
    float stormDuration = 5;
    public float GetstormDuration() { return stormDuration; }

    [Header("��ǳ ������")]
    [SerializeField]
    float stormDamageRate = 1;
    public float GetStormDamageRate() { return stormDamageRate; }

    [Header("��ǳ ���� ����")]
    [SerializeField]
    float stormDamageRadius = 1;
    public float GetStormDamageRadius() { return stormDamageRadius; }

    [Header("��ǳ ��ħ ��")]
    [SerializeField]
    float stormForce = 100f;
    public float GetStormForce() { return stormForce; }

    [Header("���� �þ� ����")]
    [SerializeField]
    float sightHindRange = 0.3f;
    public float GetSightHindRange() { return sightHindRange; }

    [Header("���� ���� �ð�")]
    [SerializeField]
    float sightHindDuration = 4f;
    public float GetsightHindDuration() { return sightHindDuration; }

    [Header("�ߵ� Ȯ��")]
    [SerializeField]
    float triggerProbability = 0.3f;
    public float GetTriggerProbability() { return triggerProbability; }
}
