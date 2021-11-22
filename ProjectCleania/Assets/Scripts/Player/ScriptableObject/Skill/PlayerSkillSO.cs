using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillSO : PlayerSkillIDSO
{
    [SerializeField]
    protected string SkillName;
    public string GetSkillName() { return SkillName; }

    [Header("Tip: 변수명을 입력할 수 있습니다.")]
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

    [Header("작동 키")]
    [SerializeField]
    protected KeyCode TriggerKey;
    public KeyCode GetTriggerKey() { return TriggerKey; }

    // public bool isAttacking;
    [Header("쿨타임")]
    [SerializeField]
    protected float CoolTime;  // 추후 private 처리
    public float GetCoolTime() { return CoolTime; }

    [Header("생성 고유 자원")]
    [SerializeField]
    protected float CreatedMP = 0f;
    public float GetCreatedMP() { return CreatedMP; }

    [Header("소모 고유 자원")]
    [SerializeField]
    protected float ConsumMP = 0f;
    public float GetConsumMP() { return ConsumMP; }

    [Header("전체 애니메이션 배속")]
    [SerializeField]
    protected float SpeedMultiplier = 1.0f;
    public float GetSpeedMultiplier() { return SpeedMultiplier; }
}
