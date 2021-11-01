using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactStayDamage : DamagingProperty
{
    private void Awake()
    {
        //if (OwnerAbility == null)
        //    throw new System.Exception("ContactDamage doesnt have OwnerAbility");
        //if ((int)DamageScale == 0)
        //    throw new System.Exception("ContactDamage DamageScale is zero");
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("중독 상태 부여!");
            //AbilityStatus playerAbil = other.gameObject.GetComponent<AbilityStatus>();
            //if (playerAbil != null)
            //    playerAbil.AttackedBy(OwnerAbility, DamageScale);
        }
    }
}
