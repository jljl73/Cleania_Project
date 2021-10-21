using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemController_v2 : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    //public int ItemCode = 1101001;
    
    ItemInstance itemInstance;
    ItemSO itemSO;

    public int h = 2;
    public int w = 1;
    const float boxSize = 64;

    public static ItemController_v2 clickedItem;

    public Vector3 prevPosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        clickedItem = this;
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();
        Vector2 curSorPoint = eventData.position;
        curSorPoint.y += 32 * (h - 1);
        eventData.position = curSorPoint;
        GameManager.Instance.MainCanvas.GetComponent<GraphicRaycaster>().Raycast(eventData, results);

        for (int i = 0; i < results.Count; ++i)
        {
            if (results[i].gameObject.tag == "Slot")
            {
                Debug.Log(results[i].gameObject.name);
                Vector3 position = results[i].gameObject.transform.position;
                position.y -= 32 * (h - 1);
                transform.position = position;
                prevPosition = position;
                break;
            }
            else if (results[i].gameObject.tag == "Panel")
                transform.position = prevPosition;
        }
    }
}
