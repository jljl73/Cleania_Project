using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondSO : EnemySkillSO
{
    [Header("���� �ð�")]
    public float Duration;
    public float GetDuration() { return Duration; }

    [Header("���� �ݰ� (������)")]
    public float Radius;
    public float GetRadius() { return Radius; }

    [Header("���� ����")]
    public float Count;
    public float GetCount() { return Count; }
}
