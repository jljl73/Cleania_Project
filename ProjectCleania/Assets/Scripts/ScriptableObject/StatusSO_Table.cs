using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusData", menuName = "Scriptable Object/Status (Table)")]
public class StatusSO_Table : ScriptableObject
{
    public float[] Atk = new float[50];
    public float[] BasicHP = new float[50];


    public float Strength = 100;
    public float Vitality = 0;
    public float Def = 0;
    public float CriticalChance = 10;
    public float CriticalScale = 200;
    public float AttackSpeed = 0.0f;
    public float MoveSpeed = 1.0f;

    public float Accuracy = 100;
    public float Dodge = 10;
    public float Tenacity = 0;

    public float BasicMP = 100;

    public float DropExp;
}
