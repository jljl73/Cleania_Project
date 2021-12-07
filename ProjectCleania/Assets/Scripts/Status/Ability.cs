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
        Chance_Percent,             // 발생 확률 연산용 (회피명중 가감, 옵션이 높고 많을수록 1에 수렴)
        PosMul_Percent,             // 확률, 비율 추가 (추가방식 : 곱)
        NegMul_Percent,             // 확률, 비율 감소 (추가방식 : 곱)
        Addition_Percent,           // 확률, 비율 추가 (추가방식 : 합)
        Addition,                   // 추가 스텟 (추가방식 : 합)

        EnumTotal
    }

    [System.Serializable]
    public struct StaticOption
    {
        public float Value;
        public Ability.Stat Stat;

        public StaticOption(Ability.Stat stat, float val)
        {
            Value = val;
            Stat = stat;
        }
    }

    [System.Serializable]
    public struct DynamicOption
    {
        public float Value;
        public Ability.Stat Stat;
        public Ability.Enhance How;
        public KeyValuePair<Ability.Stat, Ability.Enhance> Key
        { get => new KeyValuePair<Stat, Enhance>(Stat, How); }

        public DynamicOption(Ability.Stat stat, Ability.Enhance how, float val)
        {
            Value = val;
            Stat = stat;
            How = how;
        }

        public DynamicOption(KeyValuePair<Ability.Stat, Ability.Enhance> key, float val)
        {
            Value = val;
            Stat = key.Key;
            How = key.Value;
        }
    }


    public struct AffectResult
    {
        public bool Dodged;
        public bool Critical;
        public bool Heal;
        public bool Immortal;
        public float Value;
        public bool Enemy;
    }



    public static KeyValuePair<Ability.Stat, Ability.Enhance> EquipmentOptionToAbility(EquipmentOption.Option opt)
    {
        KeyValuePair<Ability.Stat, Ability.Enhance> ret;

        switch(opt)
        {
            case EquipmentOption.Option.Attack:                 // 무기공격력
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Attack, Ability.Enhance.Absolute);
                break;
            case EquipmentOption.Option.AttackSpeed:            // 공격속도
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.AttackSpeed, Ability.Enhance.Absolute);
                break;
            case EquipmentOption.Option.Strength:               // 주스탯(힘)
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Strength, Ability.Enhance.Absolute);
                break;
            case EquipmentOption.Option.IncreaseAttack:         // 공격력 증가
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Attack, Ability.Enhance.Addition_Percent);
                break;
            case EquipmentOption.Option.CriticalChance:         // 치명타확률
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.CriticalChance, Ability.Enhance.Addition);
                break;
            case EquipmentOption.Option.CriticalScale:          // 치명타피해량
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.CriticalScale, Ability.Enhance.Addition);
                break;
            case EquipmentOption.Option.Accuracy:               // 적중률
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Accuracy, Ability.Enhance.Chance_Percent);
                break;
            case EquipmentOption.Option.IncreaseDamage:         // 피해 증가
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.IncreaseDamage, Ability.Enhance.Addition_Percent);
                break;
            case EquipmentOption.Option.LifePerHit:             // 타격시 생명력회복
                //
            case EquipmentOption.Option.LifePerKill:            // 처치시 생명력회복
                //
            case EquipmentOption.Option.LifePerSecond:          // 초당 생명력 회복량
                //
            case EquipmentOption.Option.Vitality:               // 체력
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Vitality, Ability.Enhance.Absolute);
                break;
            case EquipmentOption.Option.MaxHP:                  // 생명력
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.MaxHP, Ability.Enhance.Addition_Percent);
                break;
            case EquipmentOption.Option.MaxMP:                  // 최대 고유자원
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.MaxMP, Ability.Enhance.Absolute);
                break;
            case EquipmentOption.Option.MPRestore:              // 고유자원 획득량 증가 이름 미정
                //
            case EquipmentOption.Option.DamageIncreasedNormal:  // 일반대상피해증가
                //
            case EquipmentOption.Option.DamageIncreasedElite:   // 엘리트대상피해증가
                //
            case EquipmentOption.Option.DamageIncreasedBoss:    // 보스대상피해증가
                //
            case EquipmentOption.Option.SkillCoolDown:          // 재사용대기시간감소
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.SkillCooldown, Ability.Enhance.NegMul_Percent);
                break;
            case EquipmentOption.Option.ExpIncreased:           // 경험치 획득량 증가
                //
            case EquipmentOption.Option.CleanIncreased:         // 클린 획득량 증가
                //
            case EquipmentOption.Option.Defense:                // 방어력
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Defense, Ability.Enhance.Absolute);
                break;
            case EquipmentOption.Option.ReduceDamaged:          // 피해 감소
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.ReduceDamage, Ability.Enhance.Addition_Percent);
                break;
            case EquipmentOption.Option.Tenacity:               // 강인함
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Tenacity, Ability.Enhance.Absolute);
                break;
            case EquipmentOption.Option.Dodge:                  // 회피율
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Dodge, Ability.Enhance.Chance_Percent);
                break;
            case EquipmentOption.Option.MoveSpeed:              // 이동속도
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.MoveSpeed, Ability.Enhance.Addition_Percent);
                break;
        }

        return ret;
    }

    static public float EquipmentOptionToWeight(EquipmentOption.Option opt)
    {
        switch(opt)
        {
            case EquipmentOption.Option.Attack:                 // 무기공격력
                return 1;
            case EquipmentOption.Option.AttackSpeed:            // 공격속도
                return 1;
            case EquipmentOption.Option.Strength:               // 주스탯(힘)
                return 1;
            case EquipmentOption.Option.IncreaseAttack:         // 공격력 증가
                return 0.01f;
            case EquipmentOption.Option.CriticalChance:         // 치명타확률
                return 0.01f;
            case EquipmentOption.Option.CriticalScale:          // 치명타피해량
                return 0.01f;
            case EquipmentOption.Option.Accuracy:               // 적중률
                return 0.01f;
            case EquipmentOption.Option.IncreaseDamage:         // 피해 증가
                return 0.01f;
            case EquipmentOption.Option.LifePerHit:             // 타격시 생명력회복
            case EquipmentOption.Option.LifePerKill:            // 처치시 생명력회복
            case EquipmentOption.Option.LifePerSecond:          // 초당 생명력 회복량
                break;
            case EquipmentOption.Option.Vitality:               // 생명력
                return 1;
            case EquipmentOption.Option.MaxHP:                  // 체력
                return 0.01f;
            case EquipmentOption.Option.MaxMP:                  // 최대 고유자원
                return 0.01f;
            case EquipmentOption.Option.MPRestore:              // 고유자원 획득량 증가 이름 미정
            case EquipmentOption.Option.DamageIncreasedNormal:  // 일반대상피해증가
            case EquipmentOption.Option.DamageIncreasedElite:   // 엘리트대상피해증가
            case EquipmentOption.Option.DamageIncreasedBoss:    // 보스대상피해증가
                break;
            case EquipmentOption.Option.SkillCoolDown:          // 재사용대기시간감소
                return 0.01f;
            case EquipmentOption.Option.ExpIncreased:           // 경험치 획득량 증가
            case EquipmentOption.Option.CleanIncreased:         // 클린 획득량 증가
                break;
            case EquipmentOption.Option.Defense:                // 방어력
                return 1;
            case EquipmentOption.Option.ReduceDamaged:          // 피해 감소
                return 0.01f;
            case EquipmentOption.Option.Tenacity:               // 강인함
                return 0.01f;
            case EquipmentOption.Option.Dodge:                  // 회피율
                return 0.01f;
            case EquipmentOption.Option.MoveSpeed:              // 이동속도
                return 0.01f;
        }
        return float.NaN;
    }

    public enum Buff
    {
        MoveSpeed_Buff,                 // 버프 - 이동속도
        AttackSpeed_Buff,               // 버프 - 공격속도
        Attack_Buff,                    // 버프 - 공격력
        Defense_Buff,                   // 버프 - 방어력
        Accuracy_Buff,                  // 버프 - 적중률
        CriticalChance_Buff,            // 버프 - 크리티컬 확률

        EnumTotal
    }
}
