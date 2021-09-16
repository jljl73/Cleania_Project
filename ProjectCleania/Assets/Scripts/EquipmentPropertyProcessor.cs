using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public partial class EquipmentPropertyProcessor
{
    //Equipment EquipentPropertyProcessor.GenerateEquipment(itemcode, level)

    public struct EnchantCase
    {
        public float Min;
        public float Max;
        public Ability.Stat Stat;
        public Ability.Enhance Enhance;

        public EnchantCase(float min, float max, Ability.Stat stat, Ability.Enhance how = Ability.Enhance.Absolute)
        {
            Min = min;
            Max = max;
            Stat = stat;
            Enhance = how;
        }
    }


    List<EnchantCase>[] staticCases = new List<EnchantCase>[(int)Equipment.Type.EnumTotal];
    List<EnchantCase>[] dynamicCases = new List<EnchantCase>[(int)Equipment.Type.EnumTotal];


    //      Equipment equipGenerate(level, item)   // option maximum
    
    // equipment option

}





public partial class EquipmentPropertyProcessor
{

    void InsertDefaultCases()
    {
        // main weapon
        #region
        staticCases[(int)Equipment.Type.MainWeapon].Add(new EnchantCase(1250, 1650, Ability.Stat.Attack) );
        staticCases[(int)Equipment.Type.MainWeapon].Add(new EnchantCase(1.3f, 1.3f, Ability.Stat.AttackSpeed) );
        staticCases[(int)Equipment.Type.MainWeapon].Add(new EnchantCase(650, 750, Ability.Stat.Strength) );

        dynamicCases[(int)Equipment.Type.MainWeapon].Add(new EnchantCase(0.10f, 0.15f, Ability.Stat.Attack, Ability.Enhance.Addition_Percent) );
        dynamicCases[(int)Equipment.Type.MainWeapon].Add(new EnchantCase(0.10f, 0.15f, Ability.Stat.CriticalChance, Ability.Enhance.Chance_Percent) );
        dynamicCases[(int)Equipment.Type.MainWeapon].Add(new EnchantCase(0.40f, 0.50f, Ability.Stat.CriticalScale, Ability.Enhance.Addition) );
        dynamicCases[(int)Equipment.Type.MainWeapon].Add(new EnchantCase(0.05f, 0.12f, Ability.Stat.IncreaseDamage, Ability.Enhance.Addition_Percent) );
        dynamicCases[(int)Equipment.Type.MainWeapon].Add(new EnchantCase(0.10f, 0.20f, Ability.Stat.Accuracy, Ability.Enhance.Chance_Percent) );
        //dynamicCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(10, 15, Ability.Stat...., Ability.Enhance.Addition_Percent) );
        //dynamicCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(10, 15, Ability.Stat...., Ability.Enhance.Addition_Percent) );
        dynamicCases[(int)Equipment.Type.MainWeapon].Add(new EnchantCase(650, 750, Ability.Stat.Vitality, Ability.Enhance.Addition) );
        //dynamicCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(10, 15, Ability.Stat...., Ability.Enhance.Addition_Percent) );
        dynamicCases[(int)Equipment.Type.MainWeapon].Add(new EnchantCase(10, 15, Ability.Stat.MaxMP, Ability.Enhance.Addition) );
        #endregion

        // sub weapon
        #region
        staticCases[(int)Equipment.Type.SubWeapon].Add(new EnchantCase(400, 500, Ability.Stat.Attack) );
        staticCases[(int)Equipment.Type.SubWeapon].Add(new EnchantCase(650, 750, Ability.Stat.Strength) );

        dynamicCases[(int)Equipment.Type.SubWeapon].Add(new EnchantCase(5, 10, Ability.Stat.Attack, Ability.Enhance.Addition_Percent) );
        //dynamicCases[(int)Equipment.Type.SubWeapon].Add(new Ability.Enchant(5, 10, Ability.Stat...., Ability.Enhance.Addition_Percent) );
        dynamicCases[(int)Equipment.Type.SubWeapon].Add(new EnchantCase(0.07f, 0.12f, Ability.Stat.CriticalChance, Ability.Enhance.Chance_Percent) );
        dynamicCases[(int)Equipment.Type.SubWeapon].Add(new EnchantCase(0.30f, 0.40f, Ability.Stat.CriticalScale, Ability.Enhance.Addition) );
        dynamicCases[(int)Equipment.Type.SubWeapon].Add(new EnchantCase(0.10f, 0.20f, Ability.Stat.Accuracy, Ability.Enhance.Chance_Percent) );
        //dynamicCases[(int)Equipment.Type.SubWeapon].Add(new Ability.Enchant(0.10f, 0.20f, Ability.Stat...., Ability.Enhance.Chance_Percent) );
        //dynamicCases[(int)Equipment.Type.SubWeapon].Add(new Ability.Enchant(0.10f, 0.20f, Ability.Stat...., Ability.Enhance.Chance_Percent) );
        dynamicCases[(int)Equipment.Type.SubWeapon].Add(new EnchantCase(650, 750, Ability.Stat.Vitality, Ability.Enhance.Addition) );
        dynamicCases[(int)Equipment.Type.SubWeapon].Add(new EnchantCase(0.05f, 0.10f, Ability.Stat.SkillCooldown, Ability.Enhance.NegMul_Percent) );
        #endregion

        // head
        #region
        staticCases[(int)Equipment.Type.Hat].Add(new EnchantCase(650, 750, Ability.Stat.Defense) );
        staticCases[(int)Equipment.Type.Hat].Add(new EnchantCase(650, 750, Ability.Stat.Strength) );

        dynamicCases[(int)Equipment.Type.Hat].Add(new EnchantCase(650, 750, Ability.Stat.Vitality, Ability.Enhance.Addition));
        dynamicCases[(int)Equipment.Type.Hat].Add(new EnchantCase(500, 600, Ability.Stat.Defense, Ability.Enhance.Addition));
        //dynamicCases[(int)Equipment.Type.Head].Add(new Ability.Enchant(500, 600, Ability.Stat.Defense, Ability.Enhance.Addition));
        dynamicCases[(int)Equipment.Type.Hat].Add(new EnchantCase(500, 600, Ability.Stat.Defense, Ability.Enhance.Addition));
        #endregion

        // chest
        #region
        staticCases[(int)Equipment.Type.Top].Add(new EnchantCase(650, 750, Ability.Stat.Defense) );
        staticCases[(int)Equipment.Type.Top].Add(new EnchantCase(400, 500, Ability.Stat.Strength) );
        staticCases[(int)Equipment.Type.Top].Add(new EnchantCase(400, 500, Ability.Stat.Vitality) );
        #endregion

        // legs
        #region
        staticCases[(int)Equipment.Type.Pants].Add(new EnchantCase(650, 750, Ability.Stat.Defense) );
        staticCases[(int)Equipment.Type.Pants].Add(new EnchantCase(400, 500, Ability.Stat.Strength) );
        #endregion

        // hands
        #region
        staticCases[(int)Equipment.Type.Pants].Add(new EnchantCase(500, 600, Ability.Stat.Defense) );
        staticCases[(int)Equipment.Type.Pants].Add(new EnchantCase(650, 750, Ability.Stat.Strength) );
        staticCases[(int)Equipment.Type.Pants].Add(new EnchantCase(0.10f, 0.15f, Ability.Stat.CriticalChance) );
        #endregion

        // feet
        #region
        staticCases[(int)Equipment.Type.Pants].Add(new EnchantCase(500, 600, Ability.Stat.Defense) );
        staticCases[(int)Equipment.Type.Pants].Add(new EnchantCase(400, 500, Ability.Stat.Strength) );
        staticCases[(int)Equipment.Type.Pants].Add(new EnchantCase(0.10f, 0.15f, Ability.Stat.MoveSpeed) );
        #endregion
    }

}
