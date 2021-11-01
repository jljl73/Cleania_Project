using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ChaseTrigger : MonoBehaviour
{
    public UnityAction<GameObject> OnChaseStart;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            OnChaseStart(other.gameObject);
    }
}
