using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffectIndex", menuName = "Scriptable Object/PlayerSkill/Effect/SkillEffect")]
public class SkillEffectIndexSO : ScriptableObject
{
    [Header("Skill1 = index0")]
    public int SkillIndex;
    public int GetSkillIndex() { return SkillIndex; }

    [Header("Effect1 = index0")]
    public int EffectIndex;
    public int GetEffectIndex() { return EffectIndex; }
}
