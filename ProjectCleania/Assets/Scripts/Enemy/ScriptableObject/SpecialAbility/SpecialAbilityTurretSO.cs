using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityTurret", menuName = "Scriptable Object/Enemy/SpecialAbilityTurret")]
public class SpecialAbilityTurretSO : EnemySkillSO
{
    [Header("지속 시간")]
    public float Duration;
    public float GetDuration() { return Duration; }

    [Header("생성 반경")]
    public float CreationRadius;
    public float GetCreationRadius() { return CreationRadius; }

    [Header("생성 갯수")]
    public int Count;
    public int GetCount() { return Count; }

    [Header("발사 간격")]
    public float ShotInterval;
    public float GetShotInterval() { return ShotInterval; }

    [Header("사격 가능 범위")]
    public int ShotRange;
    public int GetShotRange() { return ShotRange; }

    [Header("투사체 속도")]
    public float ProjectileSpeed;
    public float GetProjectileSpeed() { return ProjectileSpeed; }
}
