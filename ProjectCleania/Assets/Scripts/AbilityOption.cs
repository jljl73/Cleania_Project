using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityOption
{
    public enum Name
    {
        Attack_Percent = 0,                 // ���ݷ� +n%
        HeroMonsterDamage_Percent,      // ���� ��� ���� ���Ϳ��� �ִ� ���� +n%
        RareMonsterDamage_Percent,      // ��� ��� ���� ���Ϳ��� �ִ� ���� +n%
        BossMonsterDamage_Percent,      // ���� ��� ���� ���Ϳ��� �ִ� ���� +n%
        CriticalChance_Percent,         // ġ��Ÿ Ȯ�� +n%
        CriticalScale_Percent,          // ġ��Ÿ ���� +n%
        IncreaseGivingDamage_Percent,   // �ִ� ���� +n%

        Accuracy_Percent,               // ���߷� +n%
        Dodge_Percent,                  // ȸ���� n%
        Toughness_Percent,              // ������ +n%
        ReduceGettingDamage_Percent,    // �޴����� -n%

        Defense_Abs,                    // ���� +n
        Vitality_Abs,                   // ü�� +n
        HP_Percent,                     // ����� + n%
        HPperSecond_Abs,                // �ʴ� ���� ȸ�� +n
        HPperHit_Abs,                   // Ÿ�ݴ� ���� ȸ�� +n
        HPperKill_Abs,                  // óġ�� ���� ȸ�� +n

        Cooldown_Percent,               // ������ð� -n%
        MPconsumeReduce_Percent,        // �����ڿ� �Ҹ� -n%
        MaxMP_Abs,                      // �ִ� �����ڿ� +n

        EXP_Percent,                    // �����Լ� ��� ����ġ
        Gold_Percent,                   // �����Լ� ��� ��ȭ

        EquipmentOptionTotal = 22,

        MoveSpeed_Buff = 22,            // ���� - �̵��ӵ�
        AttackSpeed_Buff,               // ���� - ���ݼӵ�
        Attack_Buff,                    // ���� - ���ݷ�
        Defense_Buff,                   // ���� - ����

        AllOptionTotal
    }
}
