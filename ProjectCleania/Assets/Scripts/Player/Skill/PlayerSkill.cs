using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerSkill : Skill
{
    public enum SkillID { Dusting, Dehydration, FairysWings, Sweeping, CleaningWind, RefreshingLeapForward };
    public SkillID ID = SkillID.Dusting;

    // ¿€µø ≈∞
    protected string SkillSlotDependency = 1.ToString();
    public string GetSkillSlotDependency() { return SkillSlotDependency; }



    //public List<SkillEffectController> effectController;

    //public void PlayEffects(int effectIdx)
    //{
    //    effectController[effectIdx].PlaySkillEffect();
    //    //foreach (SkillEffectController skillEffect in effectController)
    //    //{
    //    //    skillEffect.PlaySkillEffect();
    //    //}
    //}

    //public void StopEffects(int effectIdx)
    //{
    //    effectController[effectIdx].StopSKillEffect();
    //    //foreach (SkillEffectController skillEffect in effectController)
    //    //{
    //    //    skillEffect.StopSKillEffect();
    //    //}
    //}
}
