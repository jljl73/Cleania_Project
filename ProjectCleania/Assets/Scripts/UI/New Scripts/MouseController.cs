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

    enum STATE { Default, Enemy, Entrance, Loot, BlackSmith, Merchant, Storage, Talk, Activate };
    [SerializeField]
    Texture2D[] cursorTexture;

    StringBuilder sb = new StringBuilder();
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

    STATE state = STATE.Default;
    void Start()
    {
        raycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        ChangeCursor(STATE.Default);
    }

    void Update()
    {
        // UI 마우스
        state = STATE.Default;
        if (EventSystem.current.IsPointerOverGameObject(-1))
        {
            List<RaycastResult> results = new List<RaycastResult>();
            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;
            raycaster.Raycast(pointerEventData, results);
            itemToolTip.SetActive(false);
            
            for (int i = 0; i < results.Count; ++i)
            {
                if (results[i].gameObject.CompareTag("Item"))
                {
                    itemToolTip.SetActive(true);
                    itemToolTip.transform.position = Input.mousePosition;
                    SetItemTip(results[i].gameObject);
                    switch (GameManager.Instance.uiManager.GetCurrentNPC())
                    {

                        case NPC.TYPE.Repair:
                            state = STATE.BlackSmith;
                            break;
                        case NPC.TYPE.Market:
                            state = STATE.Merchant;
                            break;
                        case NPC.TYPE.Storage:
                            state = STATE.Storage;
                            break;
                        case NPC.TYPE.Portal:
                            state = STATE.Entrance;
                            break;
                        default:
                            state = STATE.Activate;
                            break;
                    }
                }
                else if(results[i].gameObject.TryGetComponent<Button>(out Button button))
                {
                    state = STATE.Activate;
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

        ChangeCursor(state);
    }

    void ChangeCursor(STATE target)
    { 
        Cursor.SetCursor(cursorTexture[(int)target], Vector2.zero, CursorMode.Auto);
    }

    void SetItemTip(GameObject item)
    {
        ItemSO itemSO = item.GetComponent<UI_ItemController>().itemInstance.SO;

        itemName.text = itemSO.ItemName;
        sb.Clear();
        switch (itemSO.Rank)
        {
            case ItemSO.enumRank.Common:
                sb.Append("<color=white>");
                break;
            case ItemSO.enumRank.Rare:
                sb.Append("<color=blue>");
                break;
            case ItemSO.enumRank.Legendary:
                sb.Append("<color=yellow>");
                break;
        }
        sb.Append(itemSO.Rank.ToString());
        sb.Append("</color>");
        itemRank.text = sb.ToString();
        itemDetails.text = itemSO.ToolTip;

        sb.Clear();
        sb.Append(itemSO.Durability);
        sb.Append("\n");
        //sb.Append()

        ItemInstance_Equipment equipment = (ItemInstance_Equipment)item.GetComponent<UI_ItemController>().itemInstance;
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

        itemOptions.text = sb.ToString();
    }

}
