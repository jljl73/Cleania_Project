using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : DamagingProperty
{
    float moveSpeed;
    float destroyTime = 8;

    public void SetUp(float moveSpeed, AbilityStatus abil, float damageScale)
    {
        this.moveSpeed = moveSpeed;

        base.SetUp(abil, damageScale);
    }

    private void Update()
    {
        if ((int)moveSpeed == 0)
            return;

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("ÅÍ·¿ Åº¾Ë Æø¹ß!!");
            AbilityStatus playerAbil = other.gameObject.GetComponent<AbilityStatus>();
            if (playerAbil != null)
                playerAbil.AttackedBy(ownerAbility, damageScale);
            print("damageScale: " + damageScale);
            Destroy(this.gameObject);
        }
    }
}
