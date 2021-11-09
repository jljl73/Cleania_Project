using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilitySeal", menuName = "Scriptable Object/Enemy/SpecialAbilitySeal")]
public class SpecialAbilitySealSO : PondSO
{
    [Header("장판 생성 반경")]
    public float CreationRadius;
    public float GetCreationRadius() { return CreationRadius; }

    [Header("침묵 시간")]
    public float SilenceTime;
    public float GetSilenceTime() { return SilenceTime; }
}
