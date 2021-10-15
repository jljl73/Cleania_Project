using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentOption", menuName = "Scriptable Object/Equipment Option Table")]
public class EquipmentOptionSO : ScriptableObject
{
    [System.Serializable]
    public struct StaticOptionTable
    {
        public Ability.Stat Key;
        public float Min;
        public float Max;
    }

    [System.Serializable]
    public struct DynamicOptionTable
    {
        public Ability.Stat KeyStat;
        public Ability.Enhance KeyHow;
        public float Min;
        public float Max;
    }

    [SerializeField]
    StaticOptionTable[] staticTable;
    public StaticOptionTable[] StaticTable
    { get => staticTable; }

    [SerializeField]
    DynamicOptionTable[] dynamicTable;
    public DynamicOptionTable[] DynamicTable
    { get => dynamicTable; }

}
