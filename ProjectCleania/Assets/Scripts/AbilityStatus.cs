using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityStatus : MonoBehaviour
{
    Status status;          // status is essential unlike equips or buffs
    EquipmentSlot equipments;
    BuffManager buffs;

    float[] _stats = new float[(int)AbilityOption.Stat.EnumTotal];

   

    private void Awake()
    {
        status = GetComponent<Status>();
        equipments = GetComponent<EquipmentSlot>();
        buffs = GetComponent<BuffManager>();

        RefreshAll();
    }



    float RefreshStat(AbilityOption.Stat stat)
    {
        if (status == null)
            return -1;

        _stats[(int)stat] = status[stat];

        if(equipments != null)
        {
            for(AbilityOption.Enhance i = 0; i < AbilityOption.Enhance.EnumTotal; ++i)
            {
                switch(i)
                {
                    case AbilityOption.Enhance.Absolute:
                    case AbilityOption.Enhance.Addition:
                        _stats[stat] += equipments
                            break;
                }
            }
        }

        return _stats[(int)stat];
    }

    //public float TotalDamage()
    //{
    //    float value = atk * (1 + strenght * 0.01f);

    //    if (Random.Range(0, 1) < criticalChance)
    //        value *= criticalScale;

    //    return value;
    //}

    //public float AttackedBy(AbilityStatus other, float skillScale)
    //{
    //    float value = other.TotalDamage() * skillScale;


    //    //_HP -= (other.TotalDamage() )

    //    return 0;
    //}

    //void ConsumeMP(float usedMP)
    //{
    //    _MP -= usedMP;
    //}

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.A))
    //        Debug.Log(TotalDamage());
    //}
}
