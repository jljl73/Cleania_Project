using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityMine", menuName = "Scriptable Object/Enemy/SpecialAbilityMine")]
public class SpecialAbilityMineSO : PondSO
{
    [Header("���� ���� �ݰ�")]
    public float CreationRadius;
    public float GetCreationRadius() { return CreationRadius; }

    //[Header("���� ���� ��� �ð�")]
    //public float PreparationTime;
    //public float GetPreparationTime() { return PreparationTime; }
}
