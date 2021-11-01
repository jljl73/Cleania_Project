using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Division", menuName = "Scriptable Object/Enemy/DivisionSkill")]
public class DivisionSO : EnemySkillIDSO
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

    [Header("������")]
    public float DamageRate;
    public float GetDamageRate() { return DamageRate; }

    [Header("���� ����")]
    public int DividedCount;
    public int GetDividedCount() { return DividedCount; }

    [Header("��ȯ ����")]
    public float SummonRange;
    public float GetSummonRange() { return SummonRange; }

    [Header("�ߵ� Ȯ�� ex) 0.5 = 50%")]
    public float TriggerChance;
    public float GetTriggerChance() { return TriggerChance; }
}
