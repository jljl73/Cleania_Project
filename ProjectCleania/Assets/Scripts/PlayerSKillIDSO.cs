using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSKillIDSO : SkillIDSO
{
    public string SkillName;
    public string GetSkillName() { return SkillName; }

    public enum PlayerType
    {
        Blue = 1
    }

    public enum PlayerSkillType
    {
        Normal = 0,
        Ultimate = 9
    }

    public enum PlayerSkillTriggerType
    {
        SkillC = 1,
        SkillR = 2,
        Skill1 = 3,
        Skill2 = 4,
        Skill3 = 5,
        Skill4 = 6
    }

    public PlayerType playerType;
    public PlayerSkillType playerSkillType;
    public PlayerSkillTriggerType playerSkillTriggerType;

    public int ID
    {
        get
        {
            string temp = "";
            temp += ((int)organismType).ToString();
            temp += ((int)playerType).ToString();
            temp += ((int)playerSkillType).ToString();
            temp += ((int)playerSkillTriggerType).ToString();
            return int.Parse(temp);
        }
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
}
