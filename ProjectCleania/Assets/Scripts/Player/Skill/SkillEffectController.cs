using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectController : ParticleBase
{
    public void PlaySkillEffect()
    {
        PlayEffect();
    }
    
    public void StopSKillEffect()
    {
        StopEffect();
    }

    public void MovePosition(Vector3 localPose)
    {
        ParticleObject.transform.localPosition = localPose; 
    }
}
