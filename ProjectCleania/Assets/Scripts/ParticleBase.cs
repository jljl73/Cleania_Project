using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBase : MonoBehaviour
{
    public ParticleSystem ParticleObject;
    protected ParticleSystem[] particleChildrens;

    public float Scale = 1f;

    protected virtual void Awake()
    {
        particleChildrens = ParticleObject.GetComponentsInChildren<ParticleSystem>(true);
    }

    protected virtual void Start()
    {
        ChangeScalingMode(ParticleSystemScalingMode.Local);
    }

    protected void ChangeScalingMode(ParticleSystemScalingMode mode)
    {
        foreach (ParticleSystem particle in particleChildrens)
        {
            var particleMain = particle.main;
            particleMain.scalingMode = mode;
        }
    }

    protected void ChangeScale(float value)
    {
        foreach (ParticleSystem particle in particleChildrens)
        {
            particle.transform.localScale = new Vector3(value, value, value);
        }
    }
}
