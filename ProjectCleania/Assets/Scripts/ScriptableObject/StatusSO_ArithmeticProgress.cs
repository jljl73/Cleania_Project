using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatusData", menuName = "Scriptable Object/Status")]
public class StatusSO_ArithmeticProgress : ScriptableObject
{
    public float Strength = 24;
    public float LevelUpStrength = 4;

    public float Vitality = 50;
    public float LevelUpVitality = 10;

    public float Atk = 0;
    public float Def = 0;
    public float CriticalChance = 10;
    public float CriticalScale = 200;
    public float AttackSpeed = 0.0f;
    public float MoveSpeed = 1.0f;

    public float Accuracy = 100;
    public float Dodge = 10;
    public float Tenacity = 0;

    public float BasicHP = 0;
    public float BasicMP = 100;

    public float DropExp;
}
