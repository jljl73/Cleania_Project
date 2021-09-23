using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EquipmentController : MonoBehaviour
{
    int type = 0;
    public bool isEquipped;

    public void OnButtonClicked(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;


        if (pointerEventData.button == PointerEventData.InputButton.Right)
        {
            isEquipped = !isEquipped;
        }
    }

}
