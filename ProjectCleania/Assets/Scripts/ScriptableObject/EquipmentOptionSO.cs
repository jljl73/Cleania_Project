using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EquipmentOption", menuName = "Scriptable Object/Equipment Option Table")]
public class EquipmentOptionSO : ScriptableObject
{
    [SerializeField]
    Ability.Stat[] staticKey;
    public Ability.Stat[] StaticKey
    { get => staticKey; }

    [SerializeField]
    float[] staticMin;
    public float[] StaticMin
    { get => staticMin; }

    [SerializeField]
    float[] staticMax;
    public float[] StaticMax
    { get => staticMax; }


    [SerializeField]
    Ability.Stat[] dynamicKeyStat;
    public Ability.Stat[] StaticKeyStat
    { get => dynamicKeyStat; }

    [SerializeField]
    Ability.Enhance[] dynamicKeyEnhance;
    public Ability.Enhance[] StaticKeyEnhance
    { get => dynamicKeyEnhance; }

    [SerializeField]
    float[] dynamicMin;
    public float[] DynamicMin
    { get => dynamicMin; }

    [SerializeField]
    float[] dynamicMax;
    public float[] DynamicMax
    { get => dynamicMax; }
}
