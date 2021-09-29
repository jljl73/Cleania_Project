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

    public enum Enhance // ��ȭ ���
    {
        // (stat + absolute) * percent * percent * percent + addition
        // order of this enum is IMPORTANT! do not change order
        Absolute,                   // ���밪 ���� (�߰���� : ��)
        Chance_Percent,             // �߻� Ȯ�� ����� (ȸ�Ǹ��� ����, �ɼ��� ���� �������� 1�� ����)
        PosMul_Percent,             // Ȯ��, ���� �߰� (�߰���� : ��)
        NegMul_Percent,             // Ȯ��, ���� ���� (�߰���� : ��)
        Addition_Percent,           // Ȯ��, ���� �߰� (�߰���� : ��)
        Addition,                   // �߰� ���� (�߰���� : ��)

        EnumTotal
    }

    public struct Enchant
    {
        public float Value;
        public Ability.Stat Stat;
        public Ability.Enhance Enhance;

        public Enchant(float val, Ability.Stat stat, Ability.Enhance how = Ability.Enhance.Absolute)
        {
            Value = val;
            Stat = stat;
            Enhance = how;
        }
    }


    public static KeyValuePair<Ability.Stat, Ability.Enhance> EquipmentOptionToAbility(EquipmentOption.Option opt)
    {
        KeyValuePair<Ability.Stat, Ability.Enhance> ret;

        switch(opt)
        {
            case EquipmentOption.Option.Attack:                 // ������ݷ�
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Attack, Ability.Enhance.Absolute);
                break;
            case EquipmentOption.Option.AttackSpeed:            // ���ݼӵ�
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.AttackSpeed, Ability.Enhance.Absolute);
                break;
            case EquipmentOption.Option.Strength:               // �ֽ���(��)
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Strength, Ability.Enhance.Absolute);
                break;
            case EquipmentOption.Option.IncreaseAttack:         // ���ݷ� ����
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Attack, Ability.Enhance.Addition_Percent);
                break;
            case EquipmentOption.Option.CriticalChance:         // ġ��ŸȮ��
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.CriticalChance, Ability.Enhance.Addition);
                break;
            case EquipmentOption.Option.CriticalScale:          // ġ��Ÿ���ط�
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.CriticalScale, Ability.Enhance.Addition);
                break;
            case EquipmentOption.Option.Accuracy:               // ���߷�
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Accuracy, Ability.Enhance.Chance_Percent);
                break;
            case EquipmentOption.Option.IncreaseDamage:         // ���� ����
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.IncreaseDamage, Ability.Enhance.Addition_Percent);
                break;
            case EquipmentOption.Option.LifePerHit:             // Ÿ�ݽ� �����ȸ��
                //
            case EquipmentOption.Option.LifePerKill:            // óġ�� �����ȸ��
                //
            case EquipmentOption.Option.LifePerSecond:          // �ʴ� ����� ȸ����
                //
            case EquipmentOption.Option.Vitality:               // ü��
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Vitality, Ability.Enhance.Absolute);
                break;
            case EquipmentOption.Option.MaxHP:                  // �����
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.MaxHP, Ability.Enhance.Addition_Percent);
                break;
            case EquipmentOption.Option.MaxMP:                  // �ִ� �����ڿ�
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.MaxMP, Ability.Enhance.Absolute);
                break;
            case EquipmentOption.Option.MPRestore:              // �����ڿ� ȹ�淮 ���� �̸� ����
                //
            case EquipmentOption.Option.DamageIncreasedNormal:  // �Ϲݴ����������
                //
            case EquipmentOption.Option.DamageIncreasedElite:   // ����Ʈ�����������
                //
            case EquipmentOption.Option.DamageIncreasedBoss:    // ���������������
                //
            case EquipmentOption.Option.SkillCoolDown:          // ������ð�����
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.SkillCooldown, Ability.Enhance.NegMul_Percent);
                break;
            case EquipmentOption.Option.ExpIncreased:           // ����ġ ȹ�淮 ����
                //
            case EquipmentOption.Option.CleanIncreased:         // Ŭ�� ȹ�淮 ����
                //
            case EquipmentOption.Option.Defense:                // ����
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Defense, Ability.Enhance.Absolute);
                break;
            case EquipmentOption.Option.ReduceDamaged:          // ���� ����
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.ReduceDamage, Ability.Enhance.Addition_Percent);
                break;
            case EquipmentOption.Option.Tenacity:               // ������
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Tenacity, Ability.Enhance.Absolute);
                break;
            case EquipmentOption.Option.Dodge:                  // ȸ����
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.Dodge, Ability.Enhance.Chance_Percent);
                break;
            case EquipmentOption.Option.MoveSpeed:              // �̵��ӵ�
                ret = new KeyValuePair<Stat, Enhance>(Ability.Stat.MoveSpeed, Ability.Enhance.Addition_Percent);
                break;
        }

        return ret;
    }

    static public float EquipmentOptionToValue(EquipmentOption.Option opt)
    {
        switch(opt)
        {
            case EquipmentOption.Option.Attack:                 // ������ݷ�
                return 1;
            case EquipmentOption.Option.AttackSpeed:            // ���ݼӵ�
                return 1;
            case EquipmentOption.Option.Strength:               // �ֽ���(��)
                return 1;
            case EquipmentOption.Option.IncreaseAttack:         // ���ݷ� ����
                return 1;
            case EquipmentOption.Option.CriticalChance:         // ġ��ŸȮ��
            case EquipmentOption.Option.CriticalScale:          // ġ��Ÿ���ط�
            case EquipmentOption.Option.Accuracy:               // ���߷�
            case EquipmentOption.Option.IncreaseDamage:         // ���� ����
            case EquipmentOption.Option.LifePerHit:             // Ÿ�ݽ� �����ȸ��
            case EquipmentOption.Option.LifePerKill:            // óġ�� �����ȸ��
            case EquipmentOption.Option.LifePerSecond:          // �ʴ� ����� ȸ����
            case EquipmentOption.Option.Vitality:               // �����
            case EquipmentOption.Option.MaxHP:                  // ü��
            case EquipmentOption.Option.MaxMP:                  // �ִ� �����ڿ�
            case EquipmentOption.Option.MPRestore:              // �����ڿ� ȹ�淮 ���� �̸� ����
            case EquipmentOption.Option.DamageIncreasedNormal:  // �Ϲݴ����������
            case EquipmentOption.Option.DamageIncreasedElite:   // ����Ʈ�����������
            case EquipmentOption.Option.DamageIncreasedBoss:    // ���������������
            case EquipmentOption.Option.SkillCoolDown:          // ������ð�����
            case EquipmentOption.Option.ExpIncreased:           // ����ġ ȹ�淮 ����
            case EquipmentOption.Option.CleanIncreased:         // Ŭ�� ȹ�淮 ����
            case EquipmentOption.Option.Defense:                // ����
            case EquipmentOption.Option.ReduceDamaged:          // ���� ����
            case EquipmentOption.Option.Tenacity:               // ������
            case EquipmentOption.Option.Dodge:                  // ȸ����
            case EquipmentOption.Option.MoveSpeed:              // �̵��ӵ�
                return 1;
        }
        return 1;
    }

    public enum Buff
    {
        MoveSpeed_Buff,                 // ���� - �̵��ӵ�
        AttackSpeed_Buff,               // ���� - ���ݼӵ�
        Attack_Buff,                    // ���� - ���ݷ�
        Defense_Buff,                   // ���� - ����

        EnumTotal
    }
}
