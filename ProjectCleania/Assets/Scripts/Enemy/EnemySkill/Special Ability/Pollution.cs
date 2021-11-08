using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollution : ContactStayDamage
{
    [SerializeField]
    SkillEffectController effectController;

    void Start()
    {
        effectController.Scale = damageRange;
    }
}
