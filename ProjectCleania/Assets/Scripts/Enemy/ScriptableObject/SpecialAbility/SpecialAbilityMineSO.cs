using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityMine", menuName = "Scriptable Object/Enemy/SpecialAbilityMine")]
public class SpecialAbilityMineSO : PondSO
{
    [Header("생성 반경")]
    public float CreationRadius;
    public float GetCreationRadius() { return CreationRadius; }

    [Header("폭발 반경")]
    public float ExplosionRadius;
    public float GetExplosionRadius() { return ExplosionRadius; }

    //[Header("지뢰 시전 대기 시간")]
    //public float PreparationTime;
    //public float GetPreparationTime() { return PreparationTime; }
}
