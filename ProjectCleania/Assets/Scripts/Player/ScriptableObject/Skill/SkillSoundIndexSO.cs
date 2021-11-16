using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillSoundIndex", menuName = "Scriptable Object/Sound/SkillSoundIndex")]
public class SkillSoundIndexSO : ScriptableObject
{
    [SerializeField]
    int skillID;
    public int GetSkillID() { return skillID; }

    [SerializeField]
    int soundIndex;
    public int GetSoundIndex() { return soundIndex; }
}
