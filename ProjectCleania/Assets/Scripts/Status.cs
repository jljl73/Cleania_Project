using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int level = 1;

    public float level1Strength = 24;
    public float levelUpStrength = 4;
    public float strength
    {
        get => level1Strength + level * levelUpStrength;
    }
    public float level1Vitality = 50;
    public float levelUpVitality = 10;
    public float vitality
    {
        get => level1Vitality + level * levelUpVitality;
    }

    public float level1Atk = 0;
    public float atk { get => level1Atk; }
    public float level1Def = 0;
    public float def { get => level1Def; }

    public float level1CriticalChance = 10;
    public float criticalChance { get => level1CriticalChance/100; }
    public float level1CriticalScale = 200;
    public float criticalScale { get => level1CriticalScale/100; }
    public float level1MoveSpeed = 1.0f;
    public float moveSpeed { get => level1MoveSpeed; }

}
