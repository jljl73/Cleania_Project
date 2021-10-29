using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SKillSO : ScriptableObject
{
    public enum OrganismType
    {
        Player = 1,
        Monster = 2
    }

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
        Skill1 = 2,
        Skill2 = 3,
        Skill3 = 4,
        Skill4 = 5,
        SkillR = 6,
    }

    public enum MonsterType
    {
        Dusty = 1,
        WildInti = 2,
        HighDusty = 3,
        SummonerDusty = 4,
        TheDusty = 5
    }

    public enum EnemySkillTriggerType
    {
        Default = 1,
        Advanced = 2,
        Ultimate = 3
    }
}
