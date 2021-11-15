using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;


/// <summary>
/// CONTROLLER DO NOTHING WITHOUT CALLBACK & INSTANTIATING
/// </summary>
public class UI_ItemController : MonoBehaviour,
    IDragHandler, IBeginDragHandler, IEndDragHandler, // drag and drop
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
    [SerializeField]
    TextMeshProUGUI countText;

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
            //DontDestroyOnLoad(newControllerObject);
        }
        else
        {
            controller = _objectPool.Pop().GetComponent<UI_ItemController>();
            if (container != null)
                controller.transform.SetParent(container.ItemContollerParent.transform);
        }

        // refresh data of controller
        controller._ItemChange(item, container, index);
        controller.gameObject.SetActive(true);
        //controller.transform.localScale *= CanvasScaler.;

        return controller;
    }
    static public void Delete(UI_ItemController controller)
    {
        controller._ItemChange(null, null);
        _objectPool.Push(controller.gameObject);

        controller.gameObject.SetActive(false);
    }

    void _ItemChange(ItemInstance item, UI_ItemContainer container, int index = -1)
    {
        itemInstance = item;
        currentContainer = container;

        if (item != null)
        {
            itemImage.sprite = itemInstance.SO.ItemImage;

            if (item.SO.BackgroundImage == null)
                switch (item.SO.Rank)
                {
                    case ItemSO.enumRank.Common:
                        backgroundImage.sprite = Resources.Load<Sprite>("External/My Production Line/Cleric Version 2/PNGs/3 Merchant and Player Inventory/Player Inventory/1_merchant_green_item_slot");
                        break;
                    case ItemSO.enumRank.Rare:
                        backgroundImage.sprite = Resources.Load<Sprite>("External/My Production Line/Cleric Version 2/PNGs/3 Merchant and Player Inventory/Player Inventory/1_merchant_blue_item_slot");
                        break;
                    case ItemSO.enumRank.Legendary:
                        backgroundImage.sprite = Resources.Load<Sprite>("External/My Production Line/Cleric Version 2/PNGs/3 Merchant and Player Inventory/Player Inventory/1_merchant_orange_item_slot");
                        break;
                }
        else
            backgroundImage.sprite = item.SO.BackgroundImage;

            if (item.Count > 1)
            {
                countText.enabled = true;
                countText.text = item.Count.ToString();
            }
            else
            {
                countText.enabled = false;
            }
        }


        if (index >= 0 && index < container.SlotParent.transform.childCount)
        {
            if (container != null)
            {
                backgroundImage.rectTransform.sizeDelta = container.SlotParent.transform.GetChild(index).GetComponent<RectTransform>().sizeDelta;
                backgroundImage.rectTransform.position = container.SlotParent.transform.GetChild(index).GetComponent<RectTransform>().position;
                backgroundImage.rectTransform.sizeDelta = new Vector2(
                    backgroundImage.rectTransform.sizeDelta.x * item.SO.GridSize.Width,
                    backgroundImage.rectTransform.sizeDelta.y * item.SO.GridSize.Height);
            }
            prevPosition = backgroundImage.rectTransform.position;
        }
        else if (index != -1)
            Debug.LogError("Logic error in UI_ItemController : ItemChange");
    }



    // interface

    public void MoveToInventory()
    {
        ItemInstance item = itemInstance;
        bool success = GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_ItemContainer>().Add(this);

        if (!success)
            SavedData.Instance.Item_World.Add(item);
    }

    public void MoveToStorage()
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




    private void OnDestroy()
    {
        _objectPool.Clear();
    }

    // event

    void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
    {
        transform.SetAsLastSibling();

        //backgroundImage.raycastTarget = false;
        //itemImage.raycastTarget = false;
        //countText.raycastTarget = false;
        GameManager.Instance.inputManager.PlayerMovable = false;


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
        //backgroundImage.raycastTarget = true;
        //itemImage.raycastTarget = true;
        //countText.raycastTarget = true;
        GameManager.Instance.inputManager.PlayerMovable = true;


        eventData.position = backgroundImage.rectTransform.position;
        eventData.position +=
            new Vector2( backgroundImage.rectTransform.sizeDelta.x / itemInstance.SO.GridSize.Width / 2, 
                         -backgroundImage.rectTransform.sizeDelta.y / itemInstance.SO.GridSize.Height / 2);

        List<RaycastResult> results = new List<RaycastResult>();
        canvas.GetComponent<GraphicRaycaster>().Raycast(eventData, results);

        for (int i = 0; i < results.Count; ++i)
        {
            switch (results[i].gameObject.tag)
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

        StartCoroutine(_ThrowItemInInventory());

        // raycast with offset and choose action
        // if tag is slot, call Add and Remove of each containers.
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Right) return;

        switch (currentContainer.SyncWith)
        {
            case UI_ItemContainer.SyncType.Inventory:
                if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
                    _DevideItemInInventory();
                else
                    GameManager.Instance.npcManager.Dosmth(this);
                break;
            case UI_ItemContainer.SyncType.Storage:
                MoveToInventory();
                break;
            case UI_ItemContainer.SyncType.Equipment:
                MoveToInventory();
                break;
        }

        return;
    }

    IEnumerator _ThrowItemInInventory()
    {
        yield return StartCoroutine(UI_MessageBox.Instance.Show_Coroutine("버릴거야?", MessageBoxButtons.OKCancel));

        switch(UI_MessageBox.Result)
        {
            case DialogResult.OK:
                ItemInstance item = itemInstance;
                currentContainer.Remove(this);
                SavedData.Instance.Item_World.Add(item);
                break;

            case DialogResult.Cancel:
                break;
        }
    }

    IEnumerator _DevideItemInInventory()
    {
        //if (itemInstance.Count < 2)
        //{
        //    UI_MessageBox.Show("반토막내지는 말아주세요.");
        //    return;
        //}

        //ItemInstance devided = ItemInstance.Instantiate(itemInstance.SO.ID, 1);

        //if(!currentContainer.Add(devided))
        //{
        //    UI_MessageBox.Show("공간이 부족합니다.");
        //    return;
        //}
        //currentContainer.Remove(currentContainer[devided]);

        //ItemDividePanel devidePanel = currentContainer.GetComponentInChildren<ItemDividePanel>();

        //devidePanel.gameObject.SetActive(true);
        ////devidePanel.
        return null;
    }
}
