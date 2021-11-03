using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormDamage : ContactStayDamage
{
    protected override void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("0.5 초당 데미지 주기!");
            //AbilityStatus playerAbil = other.gameObject.GetComponent<AbilityStatus>();
            //if (playerAbil != null)
            //    playerAbil.AttackedBy(OwnerAbility, DamageScale);
        }
    }
}
