using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillFairysWings", menuName = "Scriptable Object/PlayerSkill/PlayerSkillFairysWings")]
public class PlayerSkillFairysWingsSO : ScriptableObject
{
    public string SkillName;
    public string GetSkillName() { return SkillName; }

    [Header("Tip: �������� �Է��� �� �ֽ��ϴ�.")]
    [TextArea]
    public string SkillDetails;

    public string GetSkillDetails()
    {
        string tempString = SkillDetails;

        string duration = Duration.ToString();
        tempString = tempString.Replace("Duration", duration);

        string speedUpRate = (SpeedUpRate * 100).ToString();
        tempString = tempString.Replace("SpeedUpRate", speedUpRate);

        string coolTime = CoolTime.ToString();
        tempString = tempString.Replace("CoolTime", coolTime);

        string createdMP = CreatedMP.ToString();
        tempString = tempString.Replace("CreatedMP", createdMP);

        string consumMP = ConsumMP.ToString();
        tempString = tempString.Replace("ConsumMP", consumMP);

        return tempString;
    }

    [Header("�۵� Ű")]
    public KeyCode TriggerKey;
    public KeyCode GetTriggerKey() { return TriggerKey; }

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

    [Header("�κ� �ִϸ��̼� ���")]
    public float HandsUpReadyMultiplier = 1.0f;
    public float GetHandsUpReadyMultiplier() { return HandsUpReadyMultiplier; }

    public float HandsUpAndDownMultiplier = 1.0f;
    public float GetHandsUpAndDownMultiplier() { return HandsUpAndDownMultiplier; }

    public float PostDelayMultiplier = 1.0f;
    public float GetPostDelayMultiplier() { return PostDelayMultiplier; }

    [Header("���� �ð�")]
    public float Duration = 5f;
    public float GetDuration() { return Duration; }

    [Header("�ӵ� ��·� (ex. 0.4 = 40% ����)")]
    public float SpeedUpRate = 1.4f;
    public float GetSpeedUpRate() { return SpeedUpRate; }


}
