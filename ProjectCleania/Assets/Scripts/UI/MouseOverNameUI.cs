using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MouseOverNameUI : NameUI, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerExit(PointerEventData eventData)
    {
        print("OnPointerExit");
        base.ActiveUI(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("OnPointerEnter");
        base.ActiveUI(true);
    }
}
