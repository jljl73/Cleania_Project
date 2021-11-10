using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pollution : ContactStayDamage
{
    [SerializeField]
    SkillEffectController effectController;

    float duration = 3f;

    void Start()
    {
        //CancelInvoke();
        //Invoke("DeactivateDelay", duration);
        effectController.Scale = damageRange * 0.3333f;
    }

    //private void OnEnable()
    //{
    //    Invoke("ReturnObject", duration);
    //}

    void DeactivateDelay() => gameObject.SetActive(false);

    void ReturnObject()
    {
        gameObject.SetActive(false);
        ObjectPool.ReturnObject(ObjectPool.enumPoolObject.Pollution, this.gameObject);
    }

    public void SetUp(float duration, AbilityStatus abil, float damageScale)
    {
        this.duration = duration;

        base.SetUp(abil, damageScale);
    }

    //private void OnDisable()
    //{
    //    //print("Im in pollution. im disabled!");
    //    ObjectPool.ReturnObject(ObjectPool.enumPoolObject.Pollution, this.gameObject);
    //    CancelInvoke();
    //}
}
