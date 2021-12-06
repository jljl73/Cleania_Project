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

    static public float EquipmentOptionToWeight(EquipmentOption.Option opt)
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
                return 0.01f;
            case EquipmentOption.Option.CriticalChance:         // ġ��ŸȮ��
                return 0.01f;
            case EquipmentOption.Option.CriticalScale:          // ġ��Ÿ���ط�
                return 0.01f;
            case EquipmentOption.Option.Accuracy:               // ���߷�
                return 0.01f;
            case EquipmentOption.Option.IncreaseDamage:         // ���� ����
                return 0.01f;
            case EquipmentOption.Option.LifePerHit:             // Ÿ�ݽ� �����ȸ��
            case EquipmentOption.Option.LifePerKill:            // óġ�� �����ȸ��
            case EquipmentOption.Option.LifePerSecond:          // �ʴ� ����� ȸ����
                break;
            case EquipmentOption.Option.Vitality:               // �����
                return 1;
            case EquipmentOption.Option.MaxHP:                  // ü��
                return 0.01f;
            case EquipmentOption.Option.MaxMP:                  // �ִ� �����ڿ�
                return 0.01f;
            case EquipmentOption.Option.MPRestore:              // �����ڿ� ȹ�淮 ���� �̸� ����
            case EquipmentOption.Option.DamageIncreasedNormal:  // �Ϲݴ����������
            case EquipmentOption.Option.DamageIncreasedElite:   // ����Ʈ�����������
            case EquipmentOption.Option.DamageIncreasedBoss:    // ���������������
                break;
            case EquipmentOption.Option.SkillCoolDown:          // ������ð�����
                return 0.01f;
            case EquipmentOption.Option.ExpIncreased:           // ����ġ ȹ�淮 ����
            case EquipmentOption.Option.CleanIncreased:         // Ŭ�� ȹ�淮 ����
                break;
            case EquipmentOption.Option.Defense:                // ����
                return 1;
            case EquipmentOption.Option.ReduceDamaged:          // ���� ����
                return 0.01f;
            case EquipmentOption.Option.Tenacity:               // ������
                return 0.01f;
            case EquipmentOption.Option.Dodge:                  // ȸ����
                return 0.01f;
            case EquipmentOption.Option.MoveSpeed:              // �̵��ӵ�
                return 0.01f;
        }
        return float.NaN;
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
