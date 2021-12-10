using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillRoll", menuName = "Scriptable Object/PlayerSkill/PlayerSkillRoll")]
public class PlayerSkillRollSO : PlayerSkillSO
{
    [Header("속도 배속")]
    public float AvoidSpeedMultiplier = 2f;
    public float GetAvoidSpeedMultiplier() { return AvoidSpeedMultiplier; }
}
