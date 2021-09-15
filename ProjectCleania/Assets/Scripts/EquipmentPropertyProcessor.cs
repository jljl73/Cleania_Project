using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class EquipmentPropertyProcessor
{ 
    static private EquipmentPropertyProcessor _singleton = null;
    static public EquipmentPropertyProcessor Instance
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = new EquipmentPropertyProcessor();
                _singleton.InsertDefaultCases();
            }

            return _singleton;
        }
    }

    List<Ability.Enchant>[] staticCases = new List<Ability.Enchant>[(int)Equipment.Type.EnumTotal];
    List<Ability.Enchant>[] dynamicCases = new List<Ability.Enchant>[(int)Equipment.Type.EnumTotal];
}





public partial class EquipmentPropertyProcessor
{

    void InsertDefaultCases()
    {
        // main weapon
        staticCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(1250, 1650, Ability.Stat.Attack) );
        staticCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(1.3f, 1.3f, Ability.Stat.AttackSpeed) );
        staticCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(650, 750, Ability.Stat.Strength) );

        dynamicCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(0.10f, 0.15f, Ability.Stat.Attack, Ability.Enhance.Addition_Percent) );
        dynamicCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(0.10f, 0.15f, Ability.Stat.CriticalChance, Ability.Enhance.Chance_Percent) );
        dynamicCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(0.40f, 0.50f, Ability.Stat.CriticalScale, Ability.Enhance.Addition) );
        dynamicCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(0.05f, 0.12f, Ability.Stat.IncreaseDamage, Ability.Enhance.Addition_Percent) );
        dynamicCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(0.10f, 0.20f, Ability.Stat.Accuracy, Ability.Enhance.Chance_Percent) );
        //dynamicCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(10, 15, Ability.Stat...., Ability.Enhance.Addition_Percent) );
        //dynamicCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(10, 15, Ability.Stat...., Ability.Enhance.Addition_Percent) );
        dynamicCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(650, 750, Ability.Stat.Vitality, Ability.Enhance.Addition) );
        //dynamicCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(10, 15, Ability.Stat...., Ability.Enhance.Addition_Percent) );
        dynamicCases[(int)Equipment.Type.MainWeapon].Add(new Ability.Enchant(10, 15, Ability.Stat.MaxMP, Ability.Enhance.Addition_Percent) );
    }

}
