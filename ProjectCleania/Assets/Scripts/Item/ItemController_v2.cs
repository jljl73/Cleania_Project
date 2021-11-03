using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ItemController_v2 : MonoBehaviour, IPointerDownHandler, IDragHandler, IEndDragHandler, IPointerClickHandler
{
    [SerializeField]
    Image image;
    Image background;
    ItemSO.enumSubCategory subCat;

    public static ItemController_v2 clickedItem;


    public ItemInstance itemInstance { get; private set; }

    Vector3 prevPosition;
    public int prevIndex = -1;
    UIManager uiManager;
    Storage parentStroage;
    public bool wearing = false;

    // generator
    static GameObject controllerPrefab;
    static Queue<GameObject> _objectPool;

    static public ItemController_v2 New(ItemInstance item, Storage inStroage)
    {
        ItemController_v2 controller = null;

        if (_objectPool == null)
            _objectPool = new Queue<GameObject>();

        if (controllerPrefab == null)
            controllerPrefab = Resources.Load<GameObject>("Prefabs/ItemController_v2");

        if (_objectPool.Count < 1)
        {
            GameObject newControllerObject = GameObject.Instantiate(controllerPrefab);
            controller = newControllerObject.GetComponent<ItemController_v2>();
        }
        else
            controller = _objectPool.Dequeue().GetComponent<ItemController_v2>();

        controller.Initialize(item, inStroage);

        controller.gameObject.SetActive(true);
        //controller.transform.localScale *= CanvasScaler.;
        return controller;
    }
    static public void Delete(ItemController_v2 controller)
    {
        controller.Initialize(null, null);
        _objectPool.Enqueue(controller.gameObject);

        controller.gameObject.SetActive(false);
    }


    public void Initialize(ItemInstance item, Storage inStorage)
    {
        itemInstance = item;
        parentStroage = inStorage;

        if (item != null && inStorage != null)
        {
            subCat = itemInstance.SO.SubCategory;
            image.sprite = itemInstance.SO.ItemImage;
            uiManager = GameManager.Instance.uiManager;
            transform.SetParent(inStorage.ItemContollerParent.transform);
        }
    }


    public void PutInventory()
    {
        GameManager.Instance.uiManager.InventoryPanel.Add(this, out prevIndex);

        //<Modified>
        if (prevIndex == -1)
            SavedData.Instance.Item_World.Add(gameObject.GetComponent<ItemController_v2>().itemInstance);
        //</Modified>
    }

    public void PullInventory()
    {
        GameManager.Instance.uiManager.InventoryPanel.Remove(prevIndex);
    }

    void MoveToInventory()
    {
        if (prevIndex != -1)
            GameManager.Instance.uiManager.StoragePanel.Remove(prevIndex);
        GameManager.Instance.uiManager.InventoryPanel.Add(this, out prevIndex);
        if (prevIndex != -1)
            parentStroage = GameManager.Instance.uiManager.InventoryPanel;
    }

    void MoveToStorage()
    {
        if (prevIndex != -1)
            GameManager.Instance.uiManager.InventoryPanel.Remove(prevIndex);
        GameManager.Instance.uiManager.StoragePanel.Add(this, out prevIndex);
        if (prevIndex != -1)
            parentStroage = GameManager.Instance.uiManager.StoragePanel;
    }

    void MoveFromParentTo(Storage to)
    {
        if (prevIndex != -1)
            parentStroage.Remove(prevIndex);
        to.Add(this, out prevIndex);
        if (prevIndex != -1)
            parentStroage = to;
    }

    public void MoveTo(Vector3 position)
    {
        prevPosition = position;
        transform.position = position;
    }


    //
    // ÀÌº¥Æ® ¸®½º³Ê
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
                parentStroage.Move(prevIndex, index);

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
            if (parentStroage == GameManager.Instance.uiManager.StoragePanel)
                MoveToInventory();
            else if (wearing == true)
                GameManager.Instance.npcManager.equpiments.Equip(this);
            else
                MoveToStorage();
        }
        else
            GameManager.Instance.npcManager.Dosmth(this);
    }
}