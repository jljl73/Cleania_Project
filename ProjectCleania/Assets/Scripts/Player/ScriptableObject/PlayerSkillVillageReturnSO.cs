using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillVillageReturn", menuName = "Scriptable Object/PlayerSkill/PlayerSkillVillageReturn")]
public class PlayerSkillVillageReturnSO : PlayerSKillIDSO
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

        return tempString;
    }

    [Header("�۵� Ű")]
    public KeyCode TriggerKey;
    public KeyCode GetTriggerKey() { return TriggerKey; }

    // public bool isAttacking;
    [Header("��Ÿ��")]
    public float CoolTime;  // ���� private ó��
    public float GetCoolTime() { return CoolTime; }

    [Header("�Ҹ� ���� �ڿ�")]
    public float ConsumMP = 0f;
    public float GetConsumMP() { return ConsumMP; }

    [Header("��ü �ִϸ��̼� ���")]
    public float SpeedMultiplier = 1.0f;
    public float GetSpeedMultiplier() { return SpeedMultiplier; }

    [Header("���� �ð�")]
    public float TimeCost = 6f;
    public float GetTimeCost() { return TimeCost; }

    [Header("��ȯ ��ǥ")]
    public Vector3 ReturnPosition;
    public Vector3 GetReturnPosition() { return ReturnPosition; }
}
