using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillFairysWings", menuName = "Scriptable Object/PlayerSkillFairysWings")]
public class PlayerSkillFairysWingsSO : ScriptableObject
{
    public string SkillName;
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

    [Header("���� �ð�")]
    public float Duration = 5f;

    [Header("�ӵ� ��·�")]
    public float SpeedUpRate = 0.4f;

    
}
