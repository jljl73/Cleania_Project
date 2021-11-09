using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


/// <summary>
/// CONTROLLER DO NOTHING WITHOUT CALLBACK & INSTANTIATING
/// </summary>
public class UI_ItemController : MonoBehaviour, 
    IDragHandler, IBeginDragHandler, IEndDragHandler, // drag and drop
    IPointerEnterHandler, IPointerExitHandler, IPointerMoveHandler,   // item infromation show
    IPointerClickHandler    // item auto move
{
    public ItemInstance itemInstance
    { get; private set; }
    public UI_ItemContainer currentContainer
    { get; private set; }
    [SerializeField]
    Image itemImage;
    [SerializeField]
    Image backgroundImage;

    Vector3 prevPosition;
    Canvas canvas;

    public bool wearing = false;

    // generator
    static GameObject _controllerPrefab;
    static Stack<GameObject> _objectPool;


    static public UI_ItemController New(ItemInstance item, UI_ItemContainer container = null, int index = -1)
    {
        // initialize statics
        if (_objectPool == null)
            _objectPool = new Stack<GameObject>();
        if (_controllerPrefab == null)
            _controllerPrefab = Resources.Load<GameObject>("Prefabs/UI_ItemController");


        UI_ItemController controller = null;

        // check object pool
        if (_objectPool.Count < 1)
        {
            GameObject newControllerObject;
            if (container != null)
                newControllerObject = GameObject.Instantiate(_controllerPrefab, container.ItemContollerParent.transform);
            else
                newControllerObject = GameObject.Instantiate(_controllerPrefab);

            controller = newControllerObject.GetComponent<UI_ItemController>();
            controller.canvas = GameManager.Instance.MainCanvas;
        }
        else
        {
            controller = _objectPool.Pop().GetComponent<UI_ItemController>();
            if (container != null)
                controller.transform.SetParent(container.ItemContollerParent.transform);
        }

        // refresh data of controller
        controller.ItemChange(item, container, index);
        controller.gameObject.SetActive(true);
        //controller.transform.localScale *= CanvasScaler.;

        return controller;
    }
    static public void Delete(UI_ItemController controller)
    {
        controller.ItemChange(null, null);
        _objectPool.Push(controller.gameObject);

        controller.gameObject.SetActive(false);
    }

    void ItemChange(ItemInstance item, UI_ItemContainer container, int index = -1)
    {
        itemInstance = item;
        currentContainer = container;

        if (item != null)
        {
            itemImage.sprite = itemInstance.SO.ItemImage;
            /*backgroundImage.sprite change code*/
        }


        if (index >= 0 && index < container.SlotParent.transform.childCount)
        {
            if (container != null)
            {
                backgroundImage.rectTransform.sizeDelta = container.SlotParent.transform.GetChild(index).GetComponent<RectTransform>().sizeDelta;
                backgroundImage.rectTransform.position = container.SlotParent.transform.GetChild(index).GetComponent<RectTransform>().position;
            }
            prevPosition = backgroundImage.rectTransform.position;
        }
        else if (index != -1)
            Debug.LogError("Logic error in UI_ItemController : ItemChange");
    }



    // interface

    public void PutInventory()
    {
        ItemInstance item = itemInstance;
        bool success = GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_ItemContainer>().Add(this);

        if (!success)
            SavedData.Instance.Item_World.Add(item);
    }

    void MoveToInventory()
    {
        PutInventory();
    }

    void MoveToStorage()
    {
        ItemInstance item = itemInstance;
        bool success = GameManager.Instance.uiManager.StoragePanel.GetComponent<UI_ItemContainer>().Add(this);

        if (!success)
            SavedData.Instance.Item_World.Add(item);
    }

    public void MoveTo(Vector3 position)
    {
        prevPosition = position;
        transform.position = position;
    }





    // event

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();

        if (currentContainer.SyncWith == UI_ItemContainer.SyncType.Equipment)
            GameManager.Instance.uiManager.InventoryPanel.transform.SetAsLastSibling();
        else
            currentContainer.transform.SetAsLastSibling();

        prevPosition = backgroundImage.transform.position;
    }

    void IDragHandler.OnDrag(PointerEventData eventData)
    {
        backgroundImage.rectTransform.anchoredPosition += eventData.delta * canvas.scaleFactor;
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        List<RaycastResult> results = new List<RaycastResult>();

        eventData.position = backgroundImage.rectTransform.position;
        canvas.GetComponent<GraphicRaycaster>().Raycast(eventData, results);

        for (int i = 0; i < results.Count; ++i)
        {
            switch(results[i].gameObject.tag)
            {
                case "Slot":
                    currentContainer.ImmigrateTo(this
                        , results[i].gameObject.GetComponentInParent<UI_ItemContainer>()
                        , results[i].gameObject.transform.GetSiblingIndex());
                    return;

                default:
                    backgroundImage.rectTransform.position = prevPosition;
                    break;
            }
        }

        // raycast with offset and choose action
        // if tag is slot, call Add and Remove of each containers.
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        // enable detail ui
        // refresh detail ui
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        // disable detail ui
    }

    void IPointerMoveHandler.OnPointerMove(PointerEventData eventData)
    {
        // ?
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        if (GameManager.Instance.uiManager.GetCurrentNPC() == NPC.TYPE.Storage)
        {
            switch (currentContainer.SyncWith)
            {
                case UI_ItemContainer.SyncType.Inventory:
                    MoveToStorage();
                    break;
                case UI_ItemContainer.SyncType.Storage:
                    MoveToInventory();
                    break;
                case UI_ItemContainer.SyncType.Equipment:
                    MoveToInventory();
                    break;
            }
        }
        else
            GameManager.Instance.npcManager.Dosmth(this);
            return;
    }
}
