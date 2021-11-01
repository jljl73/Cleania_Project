using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemController_v2 : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    public int ItemCode = 1101001;
    [SerializeField]
    Image image;
    ItemSO.enumSubCategory subCat;

    public static ItemController_v2 clickedItem;

    public ItemInstance itemInstance { get; private set; }

    Vector3 prevPosition;
    UIManager uiManager;
    bool isInStorage = false;
    public int prevIndex = -1;

    public Storage inventory;
    public Storage storage; 

    void Start()
    {
        itemInstance = ItemInstance.Instantiate(ItemCode);

        subCat = itemInstance.SO.SubCategory;
        image.sprite = itemInstance.SO.ItemImage;
        uiManager = GameManager.Instance.uiManager;
        PutInventory();
    }


    public void PutInventory()
    {
        inventory.Add(gameObject, out prevIndex);
    }

    public void PullInventory()
    {
        inventory.Remove(prevIndex);
    }

    void MoveToInventory()
    {
        if (prevIndex != -1)
            storage.Remove(prevIndex);
        inventory.Add(gameObject, out prevIndex);
        if (prevIndex != -1)
            isInStorage = false;
    }

    void MoveToStorage()
    {
        if (prevIndex != -1)
            inventory.Remove(prevIndex);
        storage.Add(gameObject, out prevIndex);
        if (prevIndex != -1)
            isInStorage = true;
    }

    public void MoveTo(Vector3 position)
    {
        prevPosition = position;
        transform.position = position;
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
                int index = results[i].gameObject.transform.GetSiblingIndex();
                if (isInStorage)
                    storage.Move(prevIndex, index);
                else
                    inventory.Move(prevIndex, index);

                prevIndex = index;
                prevPosition = transform.position;
                return;
            }
        }

        transform.position = prevPosition;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        if(uiManager.GetCurrentNPC() == NPC.TYPE.Storage)
        {
            if (isInStorage) MoveToInventory();
            else MoveToStorage();
        }
        else
            GameManager.Instance.npcManager.Dosmth(gameObject);
    }
}