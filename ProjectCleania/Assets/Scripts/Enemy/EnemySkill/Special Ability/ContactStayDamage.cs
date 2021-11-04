using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactStayDamage : DamagingProperty
{
    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("컨택 데미지!");
        }
    }
}
