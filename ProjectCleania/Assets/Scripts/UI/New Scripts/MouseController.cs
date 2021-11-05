using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseController : MonoBehaviour
{
    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
    }


    void Update()
    {
        // UI ¸¶¿ì½º
        if (EventSystem.current.IsPointerOverGameObject(-1))
        {
            List<RaycastResult> results = new List<RaycastResult>();
            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;
            raycaster.Raycast(pointerEventData, results);

            for (int i = 0; i < results.Count; ++i)
            {
                if (results[i].gameObject.CompareTag("Item"))
                {
                    
                }
            }
        }
        //
        else
        {

        }
    }
}
