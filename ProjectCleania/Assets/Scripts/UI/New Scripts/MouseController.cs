using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Text;
using TMPro;

public class MouseController : MonoBehaviour
{
    GraphicRaycaster raycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;
    RaycastHit raycastHit;
    Ray ray;

    enum CURSORSTATE { Default, Enemy, Entrance, Loot, BlackSmith, Merchant, Storage, Talk, Activate };
    [SerializeField]
    Texture2D[] cursorTexture;

    StringBuilder sb = new StringBuilder();
    [SerializeField]
    GameObject iconToolTip;
    [SerializeField]
    GameObject itemToolTip;
    [SerializeField]
    TextMeshProUGUI itemName;
    [SerializeField]
    TextMeshProUGUI itemRank;
    [SerializeField]
    TextMeshProUGUI itemDetails;
    [SerializeField]
    TextMeshProUGUI itemOptions;
    [SerializeField]
    TextMeshProUGUI hpBarText;
    [SerializeField]
    Image hpBarGauage;
    [SerializeField]
    TextMeshProUGUI itemNameOnField;


    CURSORSTATE cursorState = CURSORSTATE.Default;

    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        ChangeCursor(CURSORSTATE.Default);
    }

    void Update()
    {
        cursorState = CURSORSTATE.Default;
        itemToolTip.SetActive(false);
        iconToolTip.SetActive(false);

        if (EventSystem.current.IsPointerOverGameObject(-1))
        {
            OnUI();
        }
        else
        {
            
            OnField();   
        }

        ChangeCursor(cursorState);
    }

    void OnUI()
    {
        List<RaycastResult> results = new List<RaycastResult>();
        pointerEventData = new PointerEventData(eventSystem);
        pointerEventData.position = Input.mousePosition;
        raycaster.Raycast(pointerEventData, results);

        for (int i = 0; i < results.Count; ++i)
        {
            if (results[i].gameObject.CompareTag("Item"))
            {
                itemToolTip.SetActive(true);
                itemToolTip.transform.position = Input.mousePosition;
                SetItemTip(results[i].gameObject, out cursorState);

            }
            else if (results[i].gameObject.CompareTag("Icon"))
            {
                iconToolTip.SetActive(true);
                iconToolTip.transform.position = Input.mousePosition;
                SetIconTip(results[i].gameObject);
            }
            else if (results[i].gameObject.TryGetComponent<Button>(out Button button))
            {
                cursorState = CURSORSTATE.Activate;
            }
        }
    }

    void OnField()
    {
        hpBarGauage.transform.parent.gameObject.SetActive(false);
        itemNameOnField.transform.parent.gameObject.SetActive(false);

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        int layerMask = 1 << 6 | 1 << 15;
        if (Physics.Raycast(ray, out raycastHit, 300.0f, layerMask))
        {
            if(raycastHit.collider.CompareTag("Enemy"))
                SetEnemyTag(raycastHit.collider);
            else if (raycastHit.collider.CompareTag("DroppedItem"))
                SetDroppedItemTag(raycastHit.collider);

            //Debug.Log(raycastHit.collider.name);
        }
    }

    

    void ChangeCursor(CURSORSTATE target)
    { 
        Cursor.SetCursor(cursorTexture[(int)target], Vector2.zero, CursorMode.Auto);
    }

    void SetItemTip(GameObject item, out CURSORSTATE state)
    {
        switch (GameManager.Instance.uiManager.GetCurrentNPC())
        {
            case NPC.TYPE.Repair:
                state = CURSORSTATE.BlackSmith;
                break;
            case NPC.TYPE.Market:
                state = CURSORSTATE.Merchant;
                break;
            case NPC.TYPE.Storage:
                state = CURSORSTATE.Storage;
                break;
            case NPC.TYPE.Portal:
                state = CURSORSTATE.Entrance;
                break;
            default:
                state = CURSORSTATE.Activate;
                break;
        }

        ItemSO itemSO = item.GetComponent<UI_ItemController>().itemInstance.SO;

        itemName.text = itemSO.ItemName;
        sb.Clear();
        switch (itemSO.Rank)
        {
            case ItemSO.enumRank.Common:
                sb.Append("<color=white>");
                break;
            case ItemSO.enumRank.Rare:
                sb.Append("<color=skyblue>");
                break;
            case ItemSO.enumRank.Legendary:
                sb.Append("<color=yellow>");
                break;
        }
        sb.Append(itemSO.Rank.ToString());
        sb.Append("</color>");
        itemRank.text = sb.ToString();
        itemDetails.text = itemSO.ToolTip;


        if (!(item.GetComponent<UI_ItemController>().itemInstance is ItemInstance_Equipment))
        {
            itemOptions.gameObject.SetActive(false);
            return;
        }

        ItemInstance_Equipment equipment = (ItemInstance_Equipment)item.GetComponent<UI_ItemController>().itemInstance;

        sb.Clear();
        sb.Append("내구도 (");
        sb.Append(equipment.Durability);
        sb.Append("/");
        sb.Append(itemSO.Durability);
        sb.Append(")\n");
        //sb.Append()

        foreach (var v in equipment.StaticProperties_ToString())
        {
            sb.Append(v);
            sb.Append("\n");
        }
        foreach (var v in equipment.DynamicProperties_ToString())
        {
            sb.Append(v);
            sb.Append("\n");
        }


        sb.Append(itemSO.Disassemble ? "분해 가능\n" : "분해 불가능\n");
        sb.Append(itemSO.Tradable ? "판매 가능\n" : "판매 불가능\n");
        sb.Append(itemSO.Droppable ? "버리기 가능\n" : "버리기 불가능\n");

        itemOptions.gameObject.SetActive(true);
        itemOptions.text = sb.ToString();
    }

    void SetIconTip(GameObject icon)
    {
        iconToolTip.GetComponentInChildren<TextMeshProUGUI>().text = icon.GetComponent<IconData>().GetString();
    }

    void SetEnemyTag(Collider collider)
    {
        hpBarText.text = Enemy.GetName((int)collider.GetComponent<EnemyStateMachine>().GetMonsterType());
        hpBarGauage.fillAmount =
            collider.GetComponent<AbilityStatus>().HP /
            collider.GetComponent<AbilityStatus>()[Ability.Stat.MaxHP];
        hpBarGauage.transform.parent.gameObject.SetActive(true);
    }

    void SetDroppedItemTag(Collider collider)
    {
        sb.Clear();
        switch (collider.GetComponent<ItemObject_v2>().ItemData.SO.Rank)
        {
            case ItemSO.enumRank.Rare:
                sb.Append("<color=#4169E1>");
                break;
            case ItemSO.enumRank.Legendary:
                sb.Append("<color=#FFD700>");
                break;
            case ItemSO.enumRank.Common:
                sb.Append("<color=#FFFFFF>");
                break;
        }
        sb.Append(collider.GetComponent<ItemObject_v2>().ItemData.SO.ItemName);
        sb.Append("</color>");
        itemNameOnField.text = sb.ToString();
        itemNameOnField.transform.parent.position = Input.mousePosition;
        itemNameOnField.transform.parent.gameObject.SetActive(true);
    }
}
