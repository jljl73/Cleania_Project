using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillFairysWings", menuName = "Scriptable Object/PlayerSkill/PlayerSkillFairysWings")]
public class PlayerSkillFairysWingsSO : PlayerSkillSO
{
    [Header("���� �ð�")]
    public float Duration = 5f;
    public float GetDuration() { return Duration; }

    [Header("�ӵ� ��·� (ex. 0.4 = 40% ����)")]
    public float SpeedUpRate = 1.4f;
    public float GetSpeedUpRate() { return SpeedUpRate; }
}
