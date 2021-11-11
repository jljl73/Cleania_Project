using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollution : ContactStayDamage
{
    [SerializeField]
    SkillEffectController effectController;

    float duration;

    private void OnEnable()
    {
        Invoke("DeactivateDelay", duration);
    }

    void Start()
    {
        effectController.Scale = damageRange * 0.3333f;
    }

    void DeactivateDelay() => this.gameObject.SetActive(false);

    public void SetUp(float duration, AbilityStatus abil, float damageScale)
    {
        this.duration = duration;
        base.SetUp(abil, damageScale);
    }

    private void OnDisable()
    {
        ObjectPool.ReturnObject(ObjectPool.enumPoolObject.Pollution, this.gameObject);
        CancelInvoke();
    }
}
