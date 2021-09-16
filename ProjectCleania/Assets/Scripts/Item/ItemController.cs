using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public ItemInventory _itemInventory;
    public GameObject _inventory;
    public GameObject _clicked;
    public Canvas _canvas;
    public GraphicRaycaster _raycaster;


    Vector2 screenPoint;
    public List<GameObject> slots = new List<GameObject>();
    //public List<GameObject> Slots { get { return slots; } }

    public int h = 0;
    public int w = 0;
    public bool bCountable = false;
    public int count = 0;
    bool bChasing;

    public string KeyString = "";

    public int prevIndex { get; private set; }
    const int width = (int)ItemInventory.Size.Width;

    GameObject anotherObject = null;

    private void Awake()
    {
        transform.SetParent(_inventory.transform);
        bChasing = false;
        AutoSetting();
        MoveToSlot();
        //Debug.Log(gameObject.name + " Awake!");
        //Debug.Log(prevIndex.ToString() + "index");
    }

    void OnEnable()
    {
        MoveToSlot();
        //bChasing = false;
        //AutoSetting();
    }

    void Update()
    {
        if(bChasing)
            ChaseMouse();

    }

    public void PutInventory(ItemInventory itemInventory, GameObject inventory, GameObject clicked, Canvas canvas, GraphicRaycaster raycaster)
    {
        _itemInventory = itemInventory;
        _inventory = inventory;
        _clicked = clicked;
        _canvas = canvas;
        _raycaster = raycaster;
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
                GameObject slot = _itemInventory.getSlotPosition(
                    index + j * width + i);
                if (slot && slot.GetComponent<ItemSlot>().IsActive == false)
                    tempSlots.Add(slot);
                else
                {
                    Debug.Log(slot.GetComponent<ItemSlot>().itemController.name);
                    anotherObject = slot.GetComponent<ItemSlot>().itemController.gameObject;
                    return false;
                }
                    //return false;
            }
        }

        prevIndex = index;
        slots = tempSlots;
        return true;
    }

    // 생성시 자동 배치
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

    // OnOff 마우스따라가기
    void OnOffChasing(bool bchasing)
    {
        bChasing = bchasing;
        transform.SetParent(_clicked.transform);
    }

    // 슬롯 활성화
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

    // 슬롯 비활성화
    void DeactiveSlot()
    {
        for (int i = 0; i < slots.Count; ++i)
        {
            slots[i].GetComponent<ItemSlot>().Deactivate();
        }

        slots.Clear();
    }

    public void BackToField()
    {
        Debug.Log(gameObject.name);
        DeactiveSlot();
        gameObject.SetActive(false);
    }

    void Throw()
    {
        _itemInventory.ShowThrowPanel(this);
    }

    void Divide()
    {
        _itemInventory.ShowDividePanel(this);
    }

    public void MoveToSlot()
    {
        if (slots.Count == 0) Debug.Log(gameObject.name);

        Debug.Log(slots[0].transform.position);

        transform.position = slots[0].transform.position;
    }

    public void OnButtonDown()
    {
        if (bCountable && Input.GetKey(KeyCode.LeftShift))
            Divide();
        else
        {
            DeactiveSlot();
            OnOffChasing(true);
        }
    }

    public void OnButtonUp()
    {
        int index = GetClickedBlock();

        if (index == -1)
        {
            OnOffChasing(false);
            SetSlot(prevIndex);
            ActivateSlot();
            MoveToSlot();
            Throw();
            return;
        }

        if (SetSlot(index))
            ActivateSlot();
        else if(anotherObject)
        {
            OnOffChasing(false);
            SwapSlot(anotherObject.GetComponent<ItemController>());
        }
        else 
        {
            SetSlot(prevIndex);
            ActivateSlot();
            MoveToSlot();
        }
    }
    
    void SwapSlot(ItemController other)
    {
        int otherIndex = other.prevIndex;
        int thisIndex = this.prevIndex;

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
}
