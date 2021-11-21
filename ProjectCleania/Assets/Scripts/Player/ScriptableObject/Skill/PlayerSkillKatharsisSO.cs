using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillKatharsis", menuName = "Scriptable Object/PlayerSkill/PlayerSkillKatharsis")]
public class PlayerSkillKatharsisSO : PlayerSkillSO
{
    [Header("½ÃÀü °¡´É Ã»·®°¨%")]
    public float TriggerAvailablePercent = 0.5f;
    public float GetTriggerAvailablePercent() { return TriggerAvailablePercent; }

    [Header("ÃÊ´ç ¼Ò¸ð °íÀ¯ ÀÚ¿ø")]
    public float ConsumMPPerSec = 0f;
    public float GetConsumMPPerSec() { return ConsumMPPerSec; }

    [Header("°ø°Ý ¼Óµµ »ó½Â·ü")]
    public float AttackSpeedUpRate = 1f;
    public float GetAttackSpeedUpRate() { return AttackSpeedUpRate; }

    [Header("ÀÌµ¿ ¼Óµµ »ó½Â·ü")]
    public float MovekSpeedUpRate = 1f;
    public float GetMoveSpeedUpRate() { return MovekSpeedUpRate; }

    [Header("ÀúÇ×·Â »ó½Â·ü")]
    public float ResistanceIncreaseRate = 1f;
    public float GetResistanceIncreaseRate() { return ResistanceIncreaseRate; }

    [Header("°ø°Ý·Â »ó½Â·ü")]
    public float StrikingPowerIncreaseRate = 1.0f;
    public float GetStrikingPowerIncreaseRate() { return StrikingPowerIncreaseRate; }

    [Header("¹æ¾î·Â »ó½Â·ü")]
    public float DefenceIncreaseRate = 1.0f;
    public float GetDefenceIncreaseRate() { return DefenceIncreaseRate; }
}
