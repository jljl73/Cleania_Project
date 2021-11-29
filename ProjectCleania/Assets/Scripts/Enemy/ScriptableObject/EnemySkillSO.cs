using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnemySkill", menuName = "Scriptable Object/Enemy/EnemySkill")]
public class EnemySkillSO : EnemySkillIDSO
{
    public string SkillName;
    public string GetSkillName() { return SkillName; }

    [Header("Tip: �������� �Է��� �� �ֽ��ϴ�.")]
    [TextArea]
    public string SkillDetails;

    public string GetSkillDetails()
    {
        string tempString = SkillDetails;

        string coolTime = CoolTime.ToString();
        tempString = tempString.Replace("CoolTime", coolTime);

        string createdMP = CreatedMP.ToString();
        tempString = tempString.Replace("CreatedMP", createdMP);

        string consumMP = ConsumMP.ToString();
        tempString = tempString.Replace("ConsumMP", consumMP);

        return tempString;
    }

    [Header("�нú� ��ų")]
    public bool IsPassiveSkill = false;  // ���� private ó��
    public bool GetIsPassiveSkill() { return IsPassiveSkill; }

    // public bool isAttacking;
    [Header("��Ÿ��")]
    public float CoolTime;  // ���� private ó��
    public float GetCoolTime() { return CoolTime; }

    [Header("���� ���� �ڿ�")]
    public float CreatedMP = 0f;
    public float GetCreatedMP() { return CreatedMP; }

    [Header("�Ҹ� ���� �ڿ�")]
    public float ConsumMP = 0f;
    public float GetConsumMP() { return ConsumMP; }

    [Header("��ü �ִϸ��̼� ���")]
    public float SpeedMultiplier = 1.0f;
    public float GetSpeedMultiplier() { return SpeedMultiplier; }

    [Header("�ߵ� ���� ��ġ")]
    [SerializeField]
    Vector3 triggerPosition = Vector3.zero;
    public Vector3 GetTriggerPosition() { return triggerPosition; }

    [Header("�ߵ� ���� ����")]
    [SerializeField]
    float triggerRange = 1f;
    public float GetTriggerRange() { return triggerRange; }

    [Header("������")]
    public float DamageRate;
    public float GetDamageRate() { return DamageRate; }
}
