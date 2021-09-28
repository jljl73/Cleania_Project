using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class MouseOverDetailUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    public GameObject DetailGameObject;

    void Start()
    {
        
    }

    void Update()
    {
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("OnPointerEnter");
        DetailGameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("OnPointerExit");
        DetailGameObject.SetActive(false);
    }
}
