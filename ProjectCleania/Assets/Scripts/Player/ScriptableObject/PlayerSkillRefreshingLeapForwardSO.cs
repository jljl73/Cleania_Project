using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillRefreshingLeapForward", menuName = "Scriptable Object/PlayerSkill/PlayerSkillRefreshingLeapForward")]
public class PlayerSkillRefreshingLeapForwardSO : ScriptableObject
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

        string smashDamageRate = (SmashDamageRate * 100).ToString();
        tempString = tempString.Replace("SmashDamageRate", smashDamageRate);

        string smashRange = SmashRange.ToString();
        tempString = tempString.Replace("SmashRange", smashRange);

        string stunTime = StunTime.ToString();
        tempString = tempString.Replace("StunTime", stunTime);

        string slowTime = SlowTime.ToString();
        tempString = tempString.Replace("SlowTime", slowTime);

        return tempString;
    }

    [Header("�۵� Ű")]
    public string TriggerKey;
    public string GetTriggerKey() { return TriggerKey; }

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

    [Header("����ġ�� ������ ���� (ex. 2.0 = 200% ������ ����)")]
    public float SmashDamageRate = 5.4f;
    public float GetSmashDamageRate() { return SmashDamageRate; }

    [Header("����ġ�� ����")]
    public float SmashRange = 2f;
    public float GetSmashRange() { return SmashRange; }

    [Header("���� �ð�")]
    public float StunTime = 1.5f;
    public float GetStunTime() { return StunTime; }

    [Header("���ο� �ð�")]
    public float SlowTime = 2f;
    public float GetSlowTime() { return SlowTime; }

    [Header("���ڸ� ���� �Ÿ�")]
    public float JumpDistance = 7f;
    public float GetJumpDistance() { return JumpDistance; }


    /*
     �ݿ��� ���¸� �׸��� �������� �پ���� �� ����� ������ �����ϸ�, 540%�� ���ظ� �ݴϴ�. �ǰݵ� ������ 1.5�ʰ� ���� ���¿� ���� ��, 2�ʰ� ���ο� ���°� �˴ϴ�.
     */
}