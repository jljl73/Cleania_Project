using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemController_v2 : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public int ItemCode = 1101001;
    public ItemSO.enumSubCategory subCat;
    public static ItemController_v2 clickedItem;

    const float boxSize = 64;
    //
    ItemInstance itemInstance;
    Vector3 prevPosition;
    public int h = 2;
    public int w = 1;
    int index = -1;

    UIManager uiManager;

    void Start()
    {
        itemInstance = ItemInstance.Instantiate(ItemCode);

        h = itemInstance.SO.GridSize.Height;
        w = itemInstance.SO.GridSize.Width;
        subCat = itemInstance.SO.SubCategory;
        uiManager = GameManager.Instance.uiManager;

        //
        GameObject slot = uiManager.InventoryPanel.GetComponent<Storage>().Add(gameObject, out index);
        MoveToSlot(slot);
    }

    void MoveToSlot(GameObject slot)
    {
        Vector3 position = slot.transform.position;
        position.y -= 32 * (h - 1);
        transform.position = position;
        prevPosition = position;
    }

    // 인벤에서 창고로 옮길때
    //void InventoryToStorage()
    //{
    //    PullItemOut(uiManager.InventoryPanel.GetComponent<Storage>());
    //    PutItemIn(uiManager.StoragePanel.GetComponent<Storage>());
    //}

    //// 창고에서 인벤 옮길때
    //void StorageToInventory()
    //{
    //    PullItemOut(uiManager.StoragePanel.GetComponent<Storage>());
    //    PutItemIn(uiManager.InventoryPanel.GetComponent<Storage>());
    //}

    //// 
    //void PutItemIn(Storage storage)
    //{
    //    GameObject slot = storage.Add(this.gameObject, out index);
    //    if (slot == null) return;
    //    Debug.Log(slot.name);
    //    MoveToSlot(slot);
    //}

    //void PullItemOut(Storage storage)
    //{
    //    if(index != -1)
    //        storage.Remove(index, h, w);
    //}


    //
    // 이벤트 리스너
    //

    public void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        clickedItem = this;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;
        transform.position = eventData.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left) return;

        List<RaycastResult> results = new List<RaycastResult>();
        Vector2 curSorPoint = eventData.position;
        curSorPoint.y += 32 * (h - 1);
        eventData.position = curSorPoint;
        GameManager.Instance.MainCanvas.GetComponent<GraphicRaycaster>().Raycast(eventData, results);

        for (int i = 0; i < results.Count; ++i)
        {
            if (results[i].gameObject.tag == "Slot")
            {
                MoveToSlot(results[i].gameObject);
                return;
            }
        }
        transform.position = prevPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        clickedItem = this;
        GameManager.Instance.npcManager.Dosmth(gameObject);
    }
}