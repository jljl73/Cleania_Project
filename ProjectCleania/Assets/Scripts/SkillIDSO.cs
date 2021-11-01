using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillIDSO : ScriptableObject
{
    public enum OrganismType
    {
        Player = 1,
        Monster = 2
    }

    public OrganismType organismType;
}
