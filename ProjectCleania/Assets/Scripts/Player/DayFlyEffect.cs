using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayFlyEffect : SkillEffectController
{
    public bool DestroyAfterPlay = false;

    private void Update()
    {
        if (DestroyAfterPlay)
        {
            if (ParticleObjectWithPS != null)
            {
                if (!ParticleObjectWithPS.isPlaying)
                    Destroy(this);
            }
            else
            {
                bool isFinished = true;
                foreach (ParticleSystem particle in particleChildrens)
                {
                    if (particle.isPlaying)
                    {
                        isFinished = false;
                        break;
                    }
                }
                if (isFinished)
                    Destroy(this);
            }
        }
    }
}
