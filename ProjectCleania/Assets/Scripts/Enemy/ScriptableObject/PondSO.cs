using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PondSO : EnemySkillSO
{
    [Header("지속 시간")]
    public float Duration;
    public float GetDuration() { return Duration; }

    [Header("장판 반경 (미적용)")]
    public float Radius;
    public float GetRadius() { return Radius; }

    [Header("장판 갯수")]
    public float Count;
    public float GetCount() { return Count; }
}
