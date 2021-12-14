using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ThrowDust", menuName = "Scriptable Object/Enemy/ThrowDust")]
public class ThrowDustSO : EnemySkillSO
{
    [Header("���� ��������")]
    [SerializeField]
    float pondDamageRate = 0.5f;
    public float GetPondDamageRate() { return pondDamageRate; }

    [Header("����ü ũ��")]
    public float ProjectileSize;
    public float GetProjectileSize() { return ProjectileSize; }

    [Header("���� ���� ũ��")]
    public float PondSize;
    public float GetPondSize() { return PondSize; }
}
