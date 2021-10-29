using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AwakeDamage : MonoBehaviour
{
    public AbilityStatus OwnerAbility { get; set; }
    public float DamageScale { get; set; }

    Collider triggerCollider;

    private void Awake()
    {
        print("AwakeDamage awake");
        //if (OwnerAbility == null)
        //    throw new System.Exception("ContactDamage doesnt have OwnerAbility");
        //if ((int)DamageScale == 0)
        //    throw new System.Exception("ContactDamage DamageScale is zero");

        triggerCollider = GetComponent<Collider>();
        if (triggerCollider == null)
            throw new System.Exception("Attach collider to AwakeDamage");

        Invoke("DeactivateCollider", 0.5f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AbilityStatus playerAbil = other.gameObject.GetComponent<AbilityStatus>();
            if (playerAbil != null)
                playerAbil.AttackedBy(OwnerAbility, DamageScale);

            DeactivateCollider();
        }
    }

    void DeactivateCollider()
    {
        triggerCollider.enabled = false;
    }
}
