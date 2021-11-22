using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillPurifiedWater", menuName = "Scriptable Object/PlayerSkill/PlayerSkillPurifiedWater")]
public class PlayerSkillPurifiedWaterSO : PlayerSkillSO
{
    [Header("체력 회복율")]
    public float HPRecoveryRate = 0.6f;
    public float GetHPRecoveryRate() { return HPRecoveryRate; }

    [Header("코스트 회복률")]
    public float MPRecoveryRate = 0.3f;
    public float GetMPRecoveryRate() { return MPRecoveryRate; }
}
