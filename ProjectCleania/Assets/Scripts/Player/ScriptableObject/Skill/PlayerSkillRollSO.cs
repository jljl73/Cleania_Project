using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillRoll", menuName = "Scriptable Object/PlayerSkill/PlayerSkillRoll")]
public class PlayerSkillRollSO : PlayerSkillSO
{
    [Header("ȸ�� �Ÿ�")]
    public float AvoidDistance = 4f;
    public float GetAvoidDistance() { return AvoidDistance; }

    [Header("�ӵ� ���")]
    public float AvoidSpeedMultiplier = 2f;
    public float GetAvoidSpeedMultiplier() { return AvoidSpeedMultiplier; }
}
