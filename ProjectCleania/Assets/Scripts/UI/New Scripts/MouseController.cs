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

    CURSORSTATE cursorState = CURSORSTATE.Default;
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        ChangeCursor(CURSORSTATE.Default);
    }

    void Update()
    {
        // UI ���콺
        cursorState = CURSORSTATE.Default;
        itemToolTip.SetActive(false);
        iconToolTip.SetActive(false);

        if (EventSystem.current.IsPointerOverGameObject(-1))
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
                else if(results[i].gameObject.CompareTag("Icon"))
                {
                    iconToolTip.SetActive(true);
                    iconToolTip.transform.position = Input.mousePosition;
                    SetIconTip(results[i].gameObject);
                }
                else if(results[i].gameObject.TryGetComponent<Button>(out Button button))
                {
                    cursorState = CURSORSTATE.Activate;
                }
            }
        }
        //
        else
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit raycastHit;

            int layerMask = 1;
            if (Physics.Raycast(ray, out raycastHit, 100.0f, layerMask))
            {
            }
        }

        ChangeCursor(cursorState);
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

        ItemSO itemSO = item.GetComponent<ItemController_v2>().itemInstance.SO;

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


        if (!(item.GetComponent<ItemController_v2>().itemInstance is ItemInstance_Equipment))
        {
            itemOptions.gameObject.SetActive(false);
            return;
        }

        ItemInstance_Equipment equipment = (ItemInstance_Equipment)item.GetComponent<ItemController_v2>().itemInstance;

        sb.Clear();
        sb.Append("������ (");
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


        sb.Append(itemSO.Disassemble ? "���� ����\n" : "���� �Ұ���\n");
        sb.Append(itemSO.Tradable ? "�Ǹ� ����\n" : "�Ǹ� �Ұ���\n");
        sb.Append(itemSO.Droppable ? "������ ����\n" : "������ �Ұ���\n");

        itemOptions.gameObject.SetActive(true);
        itemOptions.text = sb.ToString();
    }

    void SetIconTip(GameObject icon)
    {
        iconToolTip.GetComponentInChildren<TextMeshProUGUI>().text = icon.GetComponent<IconData>().GetString();
    }
}
