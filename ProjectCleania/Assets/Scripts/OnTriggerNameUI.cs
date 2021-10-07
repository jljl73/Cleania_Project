using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnTriggerNameUI : NameUI
{
    protected void OnTriggerEnter(Collider other)
    {
        base.ActiveUI(true);
        //print("Trigger enter");
    }

    protected void OnTriggerExit(Collider other)
    {
        base.ActiveUI(false);
        print("Trigger exit");
    }
}
