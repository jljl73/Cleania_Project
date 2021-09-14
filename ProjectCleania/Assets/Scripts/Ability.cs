using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability
{
    public enum Stat
    {
        Strength,       // 
        Attack,         // 
        CriticalChance, // 
        CriticalScale,  // 
        AttackSpeed,    // 
        Accuracy,       // 
        IncreaseDamage, // 

        Vitality,       // 
        MaxHP,          // 
        Dodge,          // 
        Tenacity,      // 
        Defense,        // 
        ReduceDamage,   // 

        SkillCooldown,  // 
        MoveSpeed,      // 
        MaxMP,          // 

        EnumTotal
    }

    public enum Enhance // 강화 방식
    {
        // (stat + absolute) * percent * percent * percent + addition
        // order of this enum is IMPORTANT! do not change order
        Absolute,                   // 절대값 스텟 (추가방식 : 합)
        PosMul_Percent,           // 확률, 비율 추가 (추가방식 : 곱)
        NegMul_Percent,           // 확률, 비율 감소 (추가방식 : 곱)
        Addition_Percent,           // 확률, 비율 추가 (추가방식 : 합)
        Addition,                   // 추가 스텟 (추가방식 : 합)

        EnumTotal
    }

    public struct Enchant //: IEnumerable, IEnumerator
    {
        public AbilityOption.Stat stat;
        public AbilityOption.Enhance how;

        //public IEnumerator GetEnumerator()
        //{
        //    return (IEnumerator)this;
        //}

        //public object Current { get; }

        //public bool MoveNext()
        //{
        //    return false;

        //}
        //public void Reset()
        //{
        //    return;
        //}
    }

    public enum Equipment
    {
        Attack_Percent = 0,                 // 공격력 +n%
        //HeroMonsterDamage_Percent,      // 영웅 등급 이하 몬스터에게 주는 피해 +n%
        //RareMonsterDamage_Percent,      // 희귀 등급 이하 몬스터에게 주는 피해 +n%
        //BossMonsterDamage_Percent,      // 보스 등급 이하 몬스터에게 주는 피해 +n%
        CriticalChance_Percent,         // 치명타 확률 +n%
        CriticalScale_Percent,          // 치명타 피해 +n%
        //IncreaseGivingDamage_Percent,   // 주는 피해 +n%

        Accuracy_Percent,               // 적중률 +n%
        Dodge_Percent,                  // 회피율 n%
        Toughness_Percent,              // 강인함 +n%
        //ReduceGettingDamage_Percent,    // 받는피해 -n%

        Defense_Abs,                    // 방어력 +n
        Vitality_Abs,                   // 체력 +n
        HP_Percent,                     // 생명력 + n%
        //HPperSecond_Abs,                // 초당 생명 회복 +n
        //HPperHit_Abs,                   // 타격당 생명 회복 +n
        //HPperKill_Abs,                  // 처치시 생명 회복 +n

        Cooldown_Percent,               // 재사용대기시간 -n%
        MPconsumeReduce_Percent,        // 고유자원 소모량 -n%
        MaxMP_Abs,                      // 최대 고유자원 +n

        EXP_Percent,                    // 적에게서 얻는 경험치
        Gold_Percent,                   // 적에게서 얻는 재화

        EnumTotal
    }

    public enum Buff
    {
        MoveSpeed_Buff,                 // 버프 - 이동속도
        AttackSpeed_Buff,               // 버프 - 공격속도
        Attack_Buff,                    // 버프 - 공격력
        Defense_Buff,                   // 버프 - 방어력

        EnumTotal
    }
}
