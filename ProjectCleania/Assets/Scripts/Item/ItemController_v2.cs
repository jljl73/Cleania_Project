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

    public ItemInstance itemInstance { get; private set; }
    UIManager uiManager;


    public int prevIndex = -1;
    public GameObject slot;
    int storageType = -1;

    public Storage Inventory;
    public Storage storage;


    void Start()
    {
        itemInstance = ItemInstance.Instantiate(ItemCode);

        subCat = itemInstance.SO.SubCategory;
        uiManager = GameManager.Instance.uiManager;

        PutStorage();
    }

    public void MoveToSlot(GameObject slot)
    {
        Vector3 position = slot.transform.position;
        transform.position = position;
    }

    void PutStorage()
    {
        int t_index = -1;
        Remove();

        if(storageType == 0)
            slot = storage.Add(gameObject, out t_index);
        else
            slot = Inventory.Add(gameObject, out t_index);
        if (slot == null) return;

        MoveToSlot(slot);
        prevIndex = t_index;
        storageType = (storageType == 0) ? 1 : 0;
    }

    void PutStorage(int t_index)
    {
        Remove();

        if (storageType == 0)
            slot = storage.Add(gameObject, t_index);
        else
            slot = Inventory.Add(gameObject, t_index);
        if (slot == null) return;

        MoveToSlot(slot);
        prevIndex = t_index;
        storageType = (storageType == 0) ? 1 : 0;
    }


    void Remove()
    {
        if (prevIndex == -1) return;
        if (storageType == 0)
            Inventory.Remove(prevIndex);
        else
            storage.Remove(prevIndex);
    }

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

        eventData.position = curSorPoint;
        GameManager.Instance.MainCanvas.GetComponent<GraphicRaycaster>().Raycast(eventData, results);

        for (int i = 0; i < results.Count; ++i)
        {
            if (results[i].gameObject.tag == "Slot")
            {
                //나중에 추가하랠
                return;
            }
        }

        MoveToSlot(slot);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        if(GameManager.Instance.npcManager.curNPC == NPC.TYPE.Storage)
        {
            PutStorage();
        }
        else
            GameManager.Instance.npcManager.Dosmth(gameObject);



    }
}