using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class OnTriggerNameUI : NameUI
{
    private new void Awake()
    {
        base.Awake();
        FloatContainer floatContainer = GetComponent<FloatContainer>();
    }

    void OnTriggerEnter(Collider other)
    {
        base.ActiveUI(true);
        //print("Trigger enter");
    }

    private void OnTriggerStay(Collider other)
    {
        
    }

    void OnTriggerExit(Collider other)
    {
        base.ActiveUI(false);
        print("Trigger exit");
    }
}
