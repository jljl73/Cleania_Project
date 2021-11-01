using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class PlayerSkill : Skill
{
    //public enum SkillID { Dusting, Dehydration, FairysWings, Sweeping, CleaningWind, RefreshingLeapForward };
    //public SkillID ID = SkillID.Dusting;

    // ¿€µø ≈∞
    protected KeyCode SkillSlotDependency = KeyCode.Alpha1;
    public KeyCode GetSkillSlotDependency() { return SkillSlotDependency; }

    protected new void Start()
    {
        base.Start();
    }

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
