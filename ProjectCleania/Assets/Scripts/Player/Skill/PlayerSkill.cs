using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerSkill : Skill
{
    public override void DeactivateSound(int index)
    {
        GameManager.Instance.playerSoundPlayer.StopSound();
    }

    public virtual void UpdateSkillData(PlayerSkillSO skillData)
    {
        ID = skillData.ID;
        SkillName = skillData.GetSkillName();
        SkillDetails = skillData.GetSkillDetails();
        CoolTime = skillData.GetCoolTime();
        CreatedMP = skillData.GetCreatedMP();
        ConsumMP = skillData.GetConsumMP();
        SpeedMultiplier = skillData.GetSpeedMultiplier();
    }
}
