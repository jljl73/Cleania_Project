using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillKatharsis", menuName = "Scriptable Object/PlayerSkill/PlayerSkillKatharsis")]
public class PlayerSkillKatharsisSO : PlayerSKillIDSO
{
    public string SkillName;
    public string GetSkillName() { return SkillName; }

    [Header("Tip: �������� �Է��� �� �ֽ��ϴ�.")]
    [TextArea]
    public string SkillDetails;

    public string GetSkillDetails()
    {
        string tempString = SkillDetails;

        string triggerAvailablePercent = TriggerAvailablePercent.ToString();
        tempString = tempString.Replace("TriggerAvailablePercent", triggerAvailablePercent);

        

        return tempString;
    }

    [Header("�۵� Ű")]
    public KeyCode TriggerKey;
    public KeyCode GetTriggerKey() { return TriggerKey; }

    // public bool isAttacking;
    [Header("��Ÿ��")]
    public float CoolTime;  // ���� private ó��
    public float GetCoolTime() { return CoolTime; }

    [Header("���� ���� û����%")]
    public float TriggerAvailablePercent = 0.5f;
    public float GetTriggerAvailablePercent() { return TriggerAvailablePercent; }

    [Header("�ʴ� �Ҹ� ���� �ڿ�")]
    public float ConsumMPPerSec = 0f;
    public float GetConsumMPPerSec() { return ConsumMPPerSec; }

    [Header("��ü �ִϸ��̼� ���")]
    public float SpeedMultiplier = 1.0f;
    public float GetSpeedMultiplier() { return SpeedMultiplier; }

    [Header("���� �ӵ� ��·�")]
    public float AttackSpeedUpRate = 1f;
    public float GetAttackSpeedUpRate() { return AttackSpeedUpRate; }

    [Header("�̵� �ӵ� ��·�")]
    public float MovekSpeedUpRate = 1f;
    public float GetMoveSpeedUpRate() { return MovekSpeedUpRate; }

    [Header("���׷� ��·�")]
    public float ResistanceIncreaseRate = 1f;
    public float GetResistanceIncreaseRate() { return ResistanceIncreaseRate; }

    [Header("���ݷ� ��·�")]
    public float StrikingPowerIncreaseRate = 1.0f;
    public float GetStrikingPowerIncreaseRate() { return StrikingPowerIncreaseRate; }

    [Header("���� ��·�")]
    public float DefenceIncreaseRate = 1.0f;
    public float GetDefenceIncreaseRate() { return DefenceIncreaseRate; }
}
