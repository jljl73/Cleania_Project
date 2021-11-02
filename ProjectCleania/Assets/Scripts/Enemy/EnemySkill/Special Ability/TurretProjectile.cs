using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretProjectile : DamagingProperty
{
    float moveSpeed;
    float destroyTime = 8;

    public void SetUp(float moveSpeed)
    {
        this.moveSpeed = moveSpeed;
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
            //AbilityStatus playerAbil = other.gameObject.GetComponent<AbilityStatus>();
            //if (playerAbil != null)
            //    playerAbil.AttackedBy(OwnerAbility, DamageScale);
            Destroy(this.gameObject);
        }
    }
}
