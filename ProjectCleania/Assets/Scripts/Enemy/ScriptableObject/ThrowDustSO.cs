using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThrowDust", menuName = "Scriptable Object/Enemy/ThrowDust")]
public class ThrowDustSO : EnemySkillSO
{
    [Header("투사체 크기")]
    public float ProjectileSize;
    public float GetProjectileSize() { return ProjectileSize; }

    [Header("먼지 구역 크기")]
    public float PondSize;
    public float GetPondSize() { return PondSize; }
}
