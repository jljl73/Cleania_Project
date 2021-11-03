using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SpecialAbilityTurret", menuName = "Scriptable Object/Enemy/SpecialAbilityTurret")]
public class SpecialAbilityTurretSO : EnemySkillSO
{
    [Header("���� �ð�")]
    public float Duration;
    public float GetDuration() { return Duration; }

    [Header("���� �ݰ�")]
    public float CreationRadius;
    public float GetCreationRadius() { return CreationRadius; }

    [Header("���� ����")]
    public int Count;
    public int GetCount() { return Count; }

    [Header("�߻� ����")]
    public float ShotInterval;
    public float GetShotInterval() { return ShotInterval; }

    [Header("��� ���� ����")]
    public int ShotRange;
    public int GetShotRange() { return ShotRange; }

    [Header("����ü �ӵ�")]
    public float ProjectileSpeed;
    public float GetProjectileSpeed() { return ProjectileSpeed; }
}
