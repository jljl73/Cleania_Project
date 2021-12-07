using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSO : PlayerSkillIDSO
{
    [SerializeField]
    protected string SkillName;
    public string GetSkillName() { return SkillName; }

    [Header("Tip: �������� �Է��� �� �ֽ��ϴ�.")]
    [TextArea]
    [SerializeField]
    protected string SkillDetails;
    public virtual string GetSkillDetails()
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

    [Header("�۵� �ִϸ����� �Ķ����")]
    [SerializeField]
    protected string triggerParameter;
    public string GetTriggerParameter() { return triggerParameter; }

    [System.Serializable]
    public struct Rune
    {
        public Sprite sprite;
        public string RuneName;
        public string Details;
    }

    [SerializeField]
    Rune[] runes;
    public Rune[] Runes { get { return runes; } }

    //[SerializeField]
    //protected List<AudioClip> skillSound = new List<AudioClip>();
    //public AudioClip GetSkillSound(int index) { return skillSound[index]; }
    //public int GetSkillSoundCount() { return skillSound.Count; }
    
    // public bool isAttacking;
    [Header("��Ÿ��")]
    [SerializeField]
    protected float CoolTime;  // ���� private ó��
    public float GetCoolTime() { return CoolTime; }

    [Header("���� ���� �ڿ�")]
    [SerializeField]
    protected float CreatedMP = 0f;
    public float GetCreatedMP() { return CreatedMP; }

    [Header("�Ҹ� ���� �ڿ�")]
    [SerializeField]
    protected float ConsumMP = 0f;
    public float GetConsumMP() { return ConsumMP; }

    [Header("��ü �ִϸ��̼� ���")]
    [SerializeField]
    protected float SpeedMultiplier = 1.0f;
    public float GetSpeedMultiplier() { return SpeedMultiplier; }
}
