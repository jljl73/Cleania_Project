using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillFairysWings", menuName = "Scriptable Object/PlayerSkillFairysWings")]
public class PlayerSkillFairysWingsSO : ScriptableObject
{
    public string SkillName;
    [TextArea]
    public string SkillDetails;
}
