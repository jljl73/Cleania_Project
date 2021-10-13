using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillDehydration", menuName = "Scriptable Object/PlayerSkill/PlayerSkillDehydration")]
public class PlayerSkillDehydrationSO : ScriptableObject
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

        string damageRate = (DamageRate * 100).ToString();
        tempString = tempString.Replace("DamageRate", damageRate);

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
    public float DamageRate = 3.0f;

    /*
      ���ۺ��� ���� �̵��ϸ�, �̵� ��ο� �ִ� ��� ������ �ʴ� 300%��ŭ�� ���ظ� �ݴϴ�.
     */
}
