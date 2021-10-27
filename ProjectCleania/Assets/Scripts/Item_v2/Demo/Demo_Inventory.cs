using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo_Inventory : MonoBehaviour
{
    Demo_ItemSlot[] itemSlots;
    ItemStorage_LocalGrid inven;
    Equipable equipable;

    Text[] optionTexts;
    Image optionPanel;

    private void Awake()
    {
        optionTexts = GetComponentsInChildren<Text>();
        optionPanel = optionTexts[0].GetComponentInParent<Image>();
        optionPanel.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        itemSlots = GetComponentsInChildren<Demo_ItemSlot>();
        equipable = GameManager.Instance.PlayerEquipments;
        inven = SavedData.Instance.Item_Inventory;

        foreach(var i in itemSlots)
        {
            i.optionPanel = optionPanel;
            i.optionTexts = optionTexts;
        }
    }

    // Update is called once per frame
    void Update()
    {
        for(int y = 0; y <6; y++)
            for(int x = 0; x <10; x++)
            {
                if (inven[inven[y, x]] == new System.Drawing.Point(x, y))
                    itemSlots[y * 10 + x].Set(inven[y, x]);
                else
                    itemSlots[y * 10 + x].Set(null);
            }

        for (ItemInstance_Equipment.Type i = (ItemInstance_Equipment.Type)0; i < ItemInstance_Equipment.Type.EnumTotal; ++i)
        {
            itemSlots[60 + (int)i].Set(equipable[i]);
        }
    }
}
