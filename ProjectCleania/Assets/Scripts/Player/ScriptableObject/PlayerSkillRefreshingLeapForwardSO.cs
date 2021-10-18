using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillRefreshingLeapForward", menuName = "Scriptable Object/PlayerSkill/PlayerSkillRefreshingLeapForward")]
public class PlayerSkillRefreshingLeapForwardSO : ScriptableObject
{
    public string SkillName;
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

    // public bool isAttacking;
    [Header("��Ÿ��")]
    public float CoolTime;  // ���� private ó��
    public float GetCoolTime { get { return CoolTime; } }

    [Header("���� ���� �ڿ�")]
    public float CreatedMP = 0f;

    [Header("�Ҹ� ���� �ڿ�")]
    public float ConsumMP = 0f;

    [Header("��ü �ִϸ��̼� ���")]
    public float speedMultiplier = 1.0f;

    [Header("����ġ�� ������ ���� (ex. 2.0 = 200% ������ ����)")]
    public float SmashDamageRate = 5.4f;

    [Header("����ġ�� ����")]
    public float SmashRange = 2f;

    [Header("���� �ð�")]
    public float StunTime = 1.5f;

    [Header("���ο� �ð�")]
    public float SlowTime = 2f;


    /*
     �ݿ��� ���¸� �׸��� �������� �پ���� �� ����� ������ �����ϸ�, 540%�� ���ظ� �ݴϴ�. �ǰݵ� ������ 1.5�ʰ� ���� ���¿� ���� ��, 2�ʰ� ���ο� ���°� �˴ϴ�.
     */
}
