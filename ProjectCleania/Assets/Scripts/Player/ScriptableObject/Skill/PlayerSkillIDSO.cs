using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillIDSO : SkillIDSO
{
    public enum PlayerType
    {
        Blue = 1
    }

    public enum PlayerSkillType
    {
        Normal = 0,
        Ultimate = 9
    }

    //public enum PlayerSkillTriggerType
    //{
    //    SkillR = 0,
    //    SkillC = 1,
    //    SkillMouseR = 2,
    //    Skill1 = 3,
    //    Skill2 = 4,
    //    Skill3 = 5,
    //    Skill4 = 6,
    //    SkillQ = 6,
    //    SkillT = 7,
    //    skillSpace = 8,
    //    SkillF = 9
    //}

    public PlayerType playerType;
    public PlayerSkillType playerSkillType;
    //public PlayerSkillTriggerType playerSkillTriggerType;

    [Header("고유 1자리 번호")]
    [SerializeField]
    string figuresID;

    public int ID
    {
        get
        {
            string temp = "";
            temp += ((int)organismType).ToString();
            temp += ((int)playerType).ToString();
            temp += ((int)playerSkillType).ToString();
            temp += figuresID;
            return int.Parse(temp);
        }
    }

    //[System.Serializable]
    //public struct Rune
    //{
    //    public Sprite sprite;
    //    public string RuneName;
    //    public string Details;
    //}

    //[SerializeField]
    //Rune[] runes;
    //public Rune[] Runes { get { return runes; } }
}
