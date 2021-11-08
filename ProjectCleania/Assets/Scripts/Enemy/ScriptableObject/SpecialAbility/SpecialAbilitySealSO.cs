using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilitySeal", menuName = "Scriptable Object/Enemy/SpecialAbilitySeal")]
public class SpecialAbilitySealSO : PondSO
{
    [Header("���� ���� �ݰ�")]
    public float CreationRadius;
    public float GetCreationRadius() { return CreationRadius; }

    [Header("ħ�� �ð�")]
    public float SilenceTime;
    public float GetSilenceTime() { return SilenceTime; }
}
