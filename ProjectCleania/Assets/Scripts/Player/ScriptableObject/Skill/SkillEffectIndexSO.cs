using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffectIndex", menuName = "Scriptable Object/SkillEffect")]
public class SkillEffectIndexSO : ScriptableObject
{
    public int SkillID;
    public int GetSkillID() { return SkillID; }

    public int EffectID;
    public int GetEffectID() { return EffectID; }
}
