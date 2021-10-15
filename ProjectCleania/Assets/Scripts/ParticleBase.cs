using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBase : MonoBehaviour
{
    public GameObject ParticleObject;
    protected ParticleSystem[] particleChildrens;
    protected Animator[] particleChildrenAnimators;

    public float Scale = 1f;

    protected virtual void Awake()
    {
        particleChildrens = ParticleObject.GetComponentsInChildren<ParticleSystem>(true);

        particleChildrenAnimators = ParticleObject.GetComponentsInChildren<Animator>(true);

        ResetSetting();
    }

    protected virtual void Start()
    {
        ChangeScalingMode(ParticleSystemScalingMode.Local);
    }

    private void ResetSetting()
    {
        foreach (ParticleSystem particle in particleChildrens)
        {
            var particleMain = particle.main;
            particleMain.playOnAwake = false;
        }

        foreach (Animator animator in particleChildrenAnimators)
        {
            animator.enabled = false;
        }
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

    protected void PlayEffect()
    {
        ParticleSystem ParticleObjectWithPS = ParticleObject.GetComponent<ParticleSystem>();

        ChangeScale(Scale);

        if (ParticleObjectWithPS != null)
        {
            ParticleObjectWithPS.Play();
        }
        else
        {
            foreach (ParticleSystem particle in particleChildrens)
            {
                particle.Play();
            }
        }

        foreach (Animator animator in particleChildrenAnimators)
        {
            animator.enabled = true;
        }
    }

    protected void StopEffect()
    {
        ParticleSystem ParticleObjectWithPS = ParticleObject.GetComponent<ParticleSystem>();

        print("Stop effec in particleBase!");

        if (ParticleObjectWithPS != null)
        {
            ParticleObjectWithPS.Stop(true);
        }
        else
        {
            foreach (ParticleSystem particle in particleChildrens)
            {
                particle.Stop(true);
            }
        }

        foreach (Animator animator in particleChildrenAnimators)
        {
            animator.enabled = false;
        }
    }
}
