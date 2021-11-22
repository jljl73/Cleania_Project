using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillAvoid", menuName = "Scriptable Object/PlayerSkill/PlayerSkillAvoid")]
public class PlayerSkillAvoidSO : PlayerSkillSO
{
    [Header("회피 거리")]
    public float AvoidDistance = 4f;
    public float GetAvoidDistance() { return AvoidDistance; }
}
