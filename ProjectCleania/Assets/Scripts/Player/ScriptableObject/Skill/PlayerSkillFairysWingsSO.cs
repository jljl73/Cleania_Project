using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillFairysWings", menuName = "Scriptable Object/PlayerSkill/PlayerSkillFairysWings")]
public class PlayerSkillFairysWingsSO : PlayerSkillSO
{
    [Header("�κ� �ִϸ��̼� ���")]
    public float HandsUpReadyMultiplier = 1.0f;
    public float GetHandsUpReadyMultiplier() { return HandsUpReadyMultiplier; }

    public float HandsUpAndDownMultiplier = 1.0f;
    public float GetHandsUpAndDownMultiplier() { return HandsUpAndDownMultiplier; }

    public float PostDelayMultiplier = 1.0f;
    public float GetPostDelayMultiplier() { return PostDelayMultiplier; }

    [Header("���� �ð�")]
    public float Duration = 5f;
    public float GetDuration() { return Duration; }

    [Header("�ӵ� ��·� (ex. 0.4 = 40% ����)")]
    public float SpeedUpRate = 1.4f;
    public float GetSpeedUpRate() { return SpeedUpRate; }


}
