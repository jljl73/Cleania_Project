using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCMarket : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public ToggleGroup[] pages;
    public GameObject prefab_Item;
    [SerializeField]
    ScrollRect pageScroll;

    List<ItemInMarket> itemsToBuy = new List<ItemInMarket>();

    private void Awake()
    {
        ItemInMarket newItem = Instantiate(prefab_Item, pages[0].transform).GetComponent<ItemInMarket>();
        newItem.Initialize(1101001, pages[0]);
        newItem = Instantiate(prefab_Item, pages[0].transform).GetComponent<ItemInMarket>();
        newItem.Initialize(1301001, pages[0]);
        newItem = Instantiate(prefab_Item, pages[0].transform).GetComponent<ItemInMarket>();
        newItem.Initialize(1302001, pages[0]);
        newItem = Instantiate(prefab_Item, pages[0].transform).GetComponent<ItemInMarket>();
        newItem.Initialize(1303001, pages[0]);
        newItem = Instantiate(prefab_Item, pages[0].transform).GetComponent<ItemInMarket>();
        newItem.Initialize(1304001, pages[0]);
        newItem = Instantiate(prefab_Item, pages[0].transform).GetComponent<ItemInMarket>();
        newItem.Initialize(1305001, pages[0]);


        toggleGroup = pages[0];
        ShowPage(0);
    }


    public void ShowPage(int index)
    {
        for (int i = 0; i < pages.Length; ++i)
            pages[i].gameObject.SetActive(false);

        pages[index].gameObject.SetActive(true);
        toggleGroup = pages[index];
        pageScroll.content = pages[index].GetComponent<RectTransform>();
    }

    public void Buy()
    {
        // buy list reset
        var toggles = toggleGroup.ActiveToggles();
        int totalPrice = 0;
        itemsToBuy.Clear();

        foreach (Toggle t in toggles)
        {
            // 구매
            var item = t.GetComponent<ItemInMarket>();
            itemsToBuy.Add(item);
            totalPrice += ItemSO.Load(item.ItemID).Price;
        }


        // money check
        UI_Currency inventoryCurrency = GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_Currency>();
        if (inventoryCurrency.Crystal < totalPrice)
        {
            UI_MessageBox.Show("클린이 부족합니다.");
            return;
        }
        else    // enough money
        {
            UI_ItemContainer inventory = inventoryCurrency.GetComponent<UI_ItemContainer>();

            // buy one by one
            foreach (var itemToBuyInMarket in itemsToBuy)
            {

                if (inventory.Add(ItemInstance.Instantiate(itemToBuyInMarket.ItemID)))
                {
                    inventoryCurrency.AddCrystal(-ItemSO.Load(itemToBuyInMarket.ItemID).Price/* * originCount*/);
                    //if (soldItems.Contains(itemToBuyInMarket))
                    //    soldItems.Remove(itemToBuyInMarket);
                    //Destroy(itemToBuyInMarket.gameObject);
                }
                else
                {
                    //if (itemToBuyInMarket.itemInstance.Count != originCount)
                    //{
                    //    inventoryCurrency.AddCrystal(-itemToBuyInMarket.itemInstance.SO.Price * (originCount - itemToBuyInMarket.itemInstance.Count));
                    //}

                    UI_MessageBox.Show("인벤토리가 꽉 찼습니다.");
                    break;
                }

            }
            itemsToBuy.Clear();
        }

    }

    public void SellItem(UI_ItemController controller)
    {
        //if (soldItems.Count >= 10)
        //{
        //    Destroy(soldItems[0].gameObject);
        //    soldItems.RemoveAt(0);
        //}

        //ShowPage(2);
        //ItemInMarket newItem = Instantiate(prefab_Item, pages[2].transform).GetComponent<ItemInMarket>();
        //newItem.Initialize(controller.itemInstance, pages[2]);
        //soldItems.Add(newItem);

        GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_Currency>().AddCrystal(controller.itemInstance.SO.Price * controller.itemInstance.Count);
        GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_ItemContainer>().Remove(controller);

    }

}
