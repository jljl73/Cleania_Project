using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusOption
{
    public enum Option
    {
        Attack_Percent,
        HeroMonsterDamage_Percent,
        RareMonsterDamage_Percent,
        BossMonsterDamage_Percent,
        CriticalRate_Percent,
        CriticalScale_Percent,
        IncreaseGivingDamage_Percent,

        Accuracy_Percent,
        Dodge_Percent,
        Toughness_Percent,
        ReduceGettingDamage_Percent,

        Defense_Abs,
        Vitality_Abs,
        HP_Percent,
        HPperSecond_Abs,
        HPperHit_Abs,
        HPperKill_Abs,

        Cooldown_Percent,
        MPconsume_Percent,
        MaxMP_Abs,
        
        EXP_Percent,
        Gold_Percent,

        EnumTotal
    }
}
