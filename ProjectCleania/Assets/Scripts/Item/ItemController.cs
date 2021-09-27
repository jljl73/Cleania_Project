using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    ItemInventory _itemInventory;
    Canvas _canvas;

    GameObject _inventory;
    GameObject _clicked;
    GraphicRaycaster _raycaster;

    Vector2 screenPoint;

    public List<GameObject> slots = new List<GameObject>();
    //public List<GameObject> Slots { get { return slots; } }

    public int h = 0;
    public int w = 0;
    public bool bCountable = false;
    public int count = 0;

    public int PrevIndex { get; private set; }

    const int width = (int)ItemInventory.Size.Width;
    GameObject anotherObject = null;
    public ItemInventory.EquipmentType _type;

    bool bChasing;
    bool isEquipped;
    Item _item;
    public Item GetItem { get { return _item; } }

    public void Initialize(Item item)
    {
        UIManager uiManager = GameObject.Find("UIManager").GetComponent<UIManager>();
        // ���� �ʱ�ȭ
        _itemInventory = uiManager.ItemPanel.GetComponent<ItemInventory>(); 
        // �ٸ� ����� ���߿�
        _inventory = _itemInventory.transform.Find("Inventory").gameObject;
        _clicked = _itemInventory.transform.Find("Clicked").gameObject;
        _canvas = uiManager.GetCanvas;
        _raycaster = _canvas.GetComponent<GraphicRaycaster>();
        _item = item.DeepCopy();

        //�׸��� ��ġ �ʱ�ȭ
        transform.SetParent(_inventory.transform);
        OnOffChasing(false);
        if (!AutoSetting())
        { 
            _itemInventory.ShowInvenAlarmPanel();
            BackToField();
        }
    }

    void Update()
    {
        if(bChasing)
            ChaseMouse();
    }


    void ChaseMouse()
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform.parent.GetComponent<RectTransform>(),
            Input.mousePosition, _canvas.worldCamera, out screenPoint);

        transform.localPosition = screenPoint;
    }

    public int GetClickedBlock()
    {
        var ped = new PointerEventData(null);
        ped.position = Input.mousePosition;

        List<RaycastResult> results = new List<RaycastResult>();
        _raycaster.Raycast(ped, results);


        for(int i = 0; i < results.Count; ++i)
        {
            if (results[i].gameObject.tag == "Slot")
                return results[i].gameObject.GetComponent<ItemSlot>().Index;
            else if (results[i].gameObject.tag == "Panel")
                return PrevIndex;
            //Debug.Log(results[i].gameObject.tag);
        }

        return -1;
    }

    bool SetSlot(int index)
    {
        int iwidth = index % width;
        int iheight = index / width;

        if (iwidth + w > width || 
            iheight + h > (int)ItemInventory.Size.Height) return false;

        anotherObject = null;
        List<GameObject> tempSlots = new List<GameObject>();
        for (int i = 0; i < w; i++)
        {
            for (int j = 0; j < h; j++)
            {
                GameObject slot = _itemInventory.GetSlotPosition(
                    index + j * width + i);
                if (slot && slot.GetComponent<ItemSlot>().IsActive == false)
                    tempSlots.Add(slot);
                else
                {
                    anotherObject = slot.GetComponent<ItemSlot>().itemController.gameObject;
                    return false;
                }
            }
        }

        PrevIndex = index;
        slots = tempSlots;
        return true;
    }

    // ������ �ڵ� ��ġ
    bool AutoSetting()
    {
        int size = (int)ItemInventory.Size.Area;
        for (int i = 0; i < size; ++i)
        {
            if(SetSlot(i))
            {
                ActivateSlot();
                return true;
            }
        }
        return false;
    }

    // OnOff ���콺���󰡱�
    void OnOffChasing(bool bchasing)
    {
        bChasing = bchasing;
        if(bChasing)
            transform.SetParent(_clicked.transform);
    }

    // ���� Ȱ��ȭ
    void ActivateSlot()
    {
        MoveToSlot();
        bChasing = false;

        for (int i = 0; i < slots.Count; ++i)
        {
            slots[i].GetComponent<ItemSlot>().Actvivate(this);
            transform.SetParent(_inventory.transform);
        }
    }

    // ���� ��Ȱ��ȭ
    void DeactiveSlot()
    {
        for (int i = 0; i < slots.Count; ++i)
        {
            slots[i].GetComponent<ItemSlot>().Deactivate();
        }

        slots.Clear();
    }

    // ������ ������
    public void BackToField()
    {
        DeactiveSlot();
        GameObject newItem = GameObject.Find("Others").transform.Find("InventoryItemGenerator").gameObject;
        newItem.GetComponent<InventoryItemGenerator>().DropItem(_item);
        Destroy(gameObject);
    }

    void ShowThrowPanel()
    {
        _itemInventory.ShowThrowPanel(this);
    }

    void ShowDividePanel()
    {
        _itemInventory.ShowDividePanel(this);
    }

    public void MoveToSlot()
    {
        if (slots.Count == 0) { Debug.Log(gameObject.name); return; }

        transform.position = slots[0].transform.position;
    }
    
    // ���� ��ġ �ٲٱ�( �� �κ��丮 )
    void SwapSlot(ItemController other)
    {
        int otherIndex = other.PrevIndex;
        int thisIndex = this.PrevIndex;

        other.DeactiveSlot();
        this.DeactiveSlot();

        if(!other.SetSlot(thisIndex) || !SetSlot(otherIndex))
        {
            other.SetSlot(otherIndex);
            this.SetSlot(thisIndex);
        }

        other.ActivateSlot();
        this.ActivateSlot();
        other.MoveToSlot();
        this.MoveToSlot();
    }
    
    public void DestroyItem()
    {
        DeactiveSlot();
        Destroy(gameObject);
    }


    public void OnButtonDown(BaseEventData data)
    {
        PointerEventData pointerEventData = data as PointerEventData;
        if (pointerEventData.button != PointerEventData.InputButton.Left) return;

        if (bCountable && Input.GetKey(KeyCode.LeftShift))
            ShowDividePanel();
        else
        {
            DeactiveSlot();
            OnOffChasing(true);
        }
    }

    public void OnButtonUp(BaseEventData data)
    {
        PointerEventData pointerEventData = data as PointerEventData;
        if (pointerEventData.button != PointerEventData.InputButton.Left) return;

        int index = GetClickedBlock();
        //Debug.Log("index " + index.ToString());
        OnOffChasing(false);

        if (index < 0)
        {
            SetSlot(PrevIndex);
            ActivateSlot();
            ShowThrowPanel();
            return;
        }

        if (isEquipped)
        {
            TakeOff(index);
            return;
        }

        if(index - 1000 == (int)_type)
        {
            Test();
            return;
        }

        if (SetSlot(index))
            ActivateSlot();
        else if (anotherObject)
            SwapSlot(anotherObject.GetComponent<ItemController>());
        else
        {
            SetSlot(PrevIndex);
            ActivateSlot();
        }
    }

    void Wear(GameObject slot)
    {
        slots.Add(slot);
        ActivateSlot();
        isEquipped = true;
    }

    public void TakeOff(int index)
    {
        DeactiveSlot();
        if (index == -1 || index > 1000) AutoSetting();
        else SetSlot(index);
        ActivateSlot();
        isEquipped = false;
    }

    public void OnButtonClicked(BaseEventData eventData)
    {
        PointerEventData pointerEventData = eventData as PointerEventData;
        // ������ Ŭ��
        if (pointerEventData.button != PointerEventData.InputButton.Right) return;

        // �ӽ÷�
        GameObject slot = _itemInventory.GetEquipmentSlot(_type);
        DeactiveSlot();

        if (isEquipped)
        {
            TakeOff(-1);
        }
        else
        {
            if (slot.GetComponent<ItemSlot>().IsActive)
            {
                Debug.Log(PrevIndex);
                slot.GetComponent<ItemSlot>().itemController.TakeOff(PrevIndex);
            }
            Wear(slot);
        }

    }

    void Test()
    {
        GameObject slot = _itemInventory.GetEquipmentSlot(_type);
        DeactiveSlot();

        if (isEquipped)
        {
            TakeOff(-1);
        }
        else
        {
            if (slot.GetComponent<ItemSlot>().IsActive)
            {
                Debug.Log(PrevIndex);
                slot.GetComponent<ItemSlot>().itemController.TakeOff(PrevIndex);
            }
            Wear(slot);
        }
    }
}
