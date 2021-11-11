using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormWind : DamagingProperty
{
    GameObject rotatePivot;
    float rotateSpeed;

    [SerializeField]
    SkillEffectController effectController;

    private void OnEnable()
    {
        if (!isSetUp) return;

        Start();
    }

    private void OnDisable()
    {
        CancelInvoke();
        ObjectPool.ReturnObject(ObjectPool.enumPoolObject.StormWind, this.gameObject);
    }

    private void Start()
    {
        effectController.Scale = damageRange;
    }

    private void Update()
    {
        MakeWindRotateAroundMe();
    }

    public void SetUp(GameObject rotatePivot, float rotateSpeed)
    {
        this.rotatePivot = rotatePivot;
        this.rotateSpeed = rotateSpeed;
    }

    void MakeWindRotateAroundMe()
    {
        if (rotatePivot == null) return;
        transform.RotateAround(rotatePivot.transform.position, rotatePivot.transform.up, rotateSpeed * Time.deltaTime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AbilityStatus abil = other.gameObject.GetComponent<AbilityStatus>();
            if (abil != null)
                abil.AttackedBy(ownerAbility, damageScale * Time.deltaTime);
        }
    }
}
