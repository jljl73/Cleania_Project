using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillEffectIndex", menuName = "Scriptable Object/SkillEffect")]
public class SkillEffectIndexSO : ScriptableObject
{
    [SerializeField]
    int skillID;
    public int GetSkillID() { return skillID; }

    [SerializeField]
    int effectID;
    public int GetEffectID() { return effectID; }
}
