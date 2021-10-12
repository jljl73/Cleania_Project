using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerSkill : Skill
{
    public enum SkillID { Dusting, Dehydration, FairysWings, Sweeping, CleaningWind, RefreshingLeapForward };
    public SkillID ID = SkillID.Dusting;
}
