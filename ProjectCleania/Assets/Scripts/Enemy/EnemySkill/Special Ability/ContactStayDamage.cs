using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContactStayDamage : DamagingProperty
{
    protected virtual void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            print("���� ������!");
        }
    }
}
