using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkillIDSO : SkillIDSO
{
    public enum MonsterType
    {
        Dusty = 1,
        WildInti = 2,
        HighDusty = 3,
        SummonerDusty = 4,
        TheDusty = 5,
        Common = 9
    }


    public MonsterType monsterType;

    [Header("고유 2자리 번호")]
    public string DoubleFiguresID;

    public int ID
    {
        get
        {
            string temp = "";
            temp += ((int)organismType).ToString();
            temp += ((int)monsterType).ToString();
            temp += DoubleFiguresID;
            return int.Parse(temp);

        }
    }
}
