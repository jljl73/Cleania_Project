using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillEffectController : ParticleBase
{
    [SerializeField]
    int iD;

    public int Id { get => iD; set { iD = value; } }

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
