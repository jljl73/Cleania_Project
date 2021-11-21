using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillPurifiedWater", menuName = "Scriptable Object/PlayerSkill/PlayerSkillPurifiedWater")]
public class PlayerSkillPurifiedWaterSO : PlayerSkillSO
{
    [Header("ü�� ȸ����")]
    public float HPRecoveryRate = 0.6f;
    public float GetHPRecoveryRate() { return HPRecoveryRate; }

    [Header("�ڽ�Ʈ ȸ����")]
    public float MPRecoveryRate = 0.3f;
    public float GetMPRecoveryRate() { return MPRecoveryRate; }
}
