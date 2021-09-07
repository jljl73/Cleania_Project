using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Status : MonoBehaviour
{
    public int level = 1;

    float _strength = 24;
    public float strength
    {
        get => _strength + level * 4;
    }
    float _vitality = 50;
    public float vitality
    {
        get => _vitality + level * 10;
    }

    float _atk = 0;
    public float atk { get => _atk; }
    float _def = 0;
    public float def { get => _def; }

    float _criticalChance = 0.1f;
    public float criticalChance { get => _criticalChance; }
    float _criticalScale = 2.0f;
    public float criticalScale { get => _criticalScale; }
    float _moveSpeed = 1.0f;
    public float moveSpeed { get => _moveSpeed; }

}
