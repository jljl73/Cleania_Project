using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleBase : MonoBehaviour
{
    public GameObject ParticleObject;
    protected ParticleSystem[] particleChildrens;
    protected Animator[] particleChildrenAnimators;
    protected MeshRenderer[] particleChildrenMeshRenderes;

    protected ParticleSystem ParticleObjectWithPS;
    protected MeshRenderer ParticleObjectWithMR;

    [SerializeField]
    float scale = 1f;
    public float Scale { get => scale; set { scale = value; ChangeScale(value); } }

    public bool IsEnabledOnAwake = false;

    protected virtual void Awake()
    {
        particleChildrens = ParticleObject.GetComponentsInChildren<ParticleSystem>(true);

        particleChildrenAnimators = ParticleObject.GetComponentsInChildren<Animator>(true);

        particleChildrenMeshRenderes = ParticleObject.GetComponentsInChildren<MeshRenderer>(true);

        ParticleObjectWithPS = ParticleObject.GetComponent<ParticleSystem>();
        ParticleObjectWithMR = ParticleObject.GetComponent<MeshRenderer>();

        if (!IsEnabledOnAwake)
        {
            ResetSetting();
            ParticleObject.SetActive(false);
        }
    }

    protected virtual void Start()
    {
        if (ParticleObjectWithMR != null)
            ParticleObjectWithMR.enabled = false;

        ChangeScalingMode(ParticleSystemScalingMode.Local);
        ChangeScale(Scale);
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

        foreach (MeshRenderer ms in particleChildrenMeshRenderes)
        {
            ms.enabled = false;
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
        //if (ParticleObjectWithMR != null)
        //{
        //    Vector3 changedScale = new Vector3(value, value, value);
        //    ParticleObject.transform.localScale = changedScale;
        //}

        Vector3 changedScale = new Vector3(value, value, value);
        ParticleObject.transform.localScale = changedScale;

        foreach (ParticleSystem particle in particleChildrens)
        {
            particle.transform.localScale = new Vector3(value, value, value);
        }
    }

    protected void PlayEffect()
    {
        ParticleObject.SetActive(true);

        ChangeScale(Scale);

        if (ParticleObjectWithMR != null)
            ParticleObjectWithMR.enabled = true;

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

        foreach (MeshRenderer ms in particleChildrenMeshRenderes)
        {
            ms.enabled = true;
        }

        foreach (Animator animator in particleChildrenAnimators)
        {
            animator.enabled = true;
        }
    }

    protected void StopEffect()
    {
        ParticleSystem ParticleObjectWithPS = ParticleObject.GetComponent<ParticleSystem>();

        if (ParticleObjectWithMR != null)
            ParticleObjectWithMR.enabled = false;

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

        foreach (MeshRenderer ms in particleChildrenMeshRenderes)
        {
            ms.enabled = false;
        }

        foreach (Animator animator in particleChildrenAnimators)
        {
            animator.enabled = false;
        }

        ParticleObject.SetActive(false);
    }
}
