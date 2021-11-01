using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactOnceDamage : DamagingProperty
{
    Collider triggerCollider;
    public float ColliderEnableTime = 1;

    private void Awake()
    {
        triggerCollider = GetComponent<Collider>();
        if (triggerCollider == null)
            throw new System.Exception("ContactOnceDamage doesnt have collider");

        Invoke("EnableCollider", ColliderEnableTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("Áö·Ú Æø¹ß!!");
            //AbilityStatus playerAbil = other.gameObject.GetComponent<AbilityStatus>();
            //if (playerAbil != null)
            //    playerAbil.AttackedBy(OwnerAbility, DamageScale);
            Destroy(this.gameObject);
        }
    }

    void EnableCollider()
    {
        triggerCollider.enabled = true;
    }
}
