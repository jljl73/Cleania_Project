using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class InteractionObject : MonoBehaviour
{
    protected bool isUsed = false;
    public bool IsUsed { get { return isUsed; } }

    public abstract void UsedTo(Collider target);

    public void Reset()
    {
        isUsed = false;
    }

    protected void OnTriggerStay(Collider other)
    {
        if (isUsed)
            return;

        if (other.CompareTag("Player") && Input.GetKey(KeyCode.G))
        {
            // PopUpDetail
            this.UsedTo(other);
            isUsed = true;
        }
    }
}
