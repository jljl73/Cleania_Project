using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOption
{
    public enum Stat
    {
        Strength,      
        Attack,        
        CriticalChance,
        CriticalScale, 
        AttackSpeed,
        Accuracy,
        IncreaseDamage,

        Vitality,
        MaxHP,   
        Dodge,
        Toughness,
        Defense, 
        ReduceDamage,
        
        SkillCooldown,
        MoveSpeed,
        MaxMP,   

        EnumTotal
    }

    public enum Enhance
    {
        //(stat + absolute) * percent * percent * percent + addition
        Absolute,
        PosMulti_Percent,
        NegMulti_Percent,
        Addition_Percent,
        Addition,

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
        Attack_Percent = 0,                 // ���ݷ� +n%
        //HeroMonsterDamage_Percent,      // ���� ��� ���� ���Ϳ��� �ִ� ���� +n%
        //RareMonsterDamage_Percent,      // ��� ��� ���� ���Ϳ��� �ִ� ���� +n%
        //BossMonsterDamage_Percent,      // ���� ��� ���� ���Ϳ��� �ִ� ���� +n%
        CriticalChance_Percent,         // ġ��Ÿ Ȯ�� +n%
        CriticalScale_Percent,          // ġ��Ÿ ���� +n%
        //IncreaseGivingDamage_Percent,   // �ִ� ���� +n%

        Accuracy_Percent,               // ���߷� +n%
        Dodge_Percent,                  // ȸ���� n%
        Toughness_Percent,              // ������ +n%
        //ReduceGettingDamage_Percent,    // �޴����� -n%

        Defense_Abs,                    // ���� +n
        Vitality_Abs,                   // ü�� +n
        HP_Percent,                     // ����� + n%
        //HPperSecond_Abs,                // �ʴ� ���� ȸ�� +n
        //HPperHit_Abs,                   // Ÿ�ݴ� ���� ȸ�� +n
        //HPperKill_Abs,                  // óġ�� ���� ȸ�� +n

        Cooldown_Percent,               // ������ð� -n%
        MPconsumeReduce_Percent,        // �����ڿ� �Ҹ� -n%
        MaxMP_Abs,                      // �ִ� �����ڿ� +n

        EXP_Percent,                    // �����Լ� ��� ����ġ
        Gold_Percent,                   // �����Լ� ��� ��ȭ

        EnumTotal
    }

    public enum Buff
    {
        MoveSpeed_Buff = 0,                 // ���� - �̵��ӵ�
        AttackSpeed_Buff,               // ���� - ���ݼӵ�
        Attack_Buff,                    // ���� - ���ݷ�
        Defense_Buff,                   // ���� - ����

        EnumTotal
    }
}
