using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCMarket : MonoBehaviour
{
    [SerializeField]
    ItemSO[] Items1;
    [SerializeField]
    ItemSO[] Items2;
    [SerializeField]
    ItemSO[] Items3;

    [SerializeField]
    ToggleGroup toggleGroup;
    [SerializeField]
    ToggleGroup[] pages;
    [SerializeField]
    GameObject prefab_Item;
    [SerializeField]
    ScrollRect pageScroll;

    List<ItemInMarket> itemsToBuy = new List<ItemInMarket>();

    private void Awake()
    {
        if (Items1 != null && Items1.Length != 0)
            foreach (var i in Items1)
            {
                ItemInMarket Item = Instantiate(prefab_Item, pages[0].transform).GetComponent<ItemInMarket>();
                Item.Initialize(i, pages[0]);
            }

        if (Items2 != null && Items2.Length != 0)
            foreach (var i in Items2)
            {
                ItemInMarket Item = Instantiate(prefab_Item, pages[1].transform).GetComponent<ItemInMarket>();
                Item.Initialize(i, pages[1]);
            }

        if (Items3 != null && Items3.Length != 0)
            foreach (var i in Items3)
            {
                ItemInMarket Item = Instantiate(prefab_Item, pages[2].transform).GetComponent<ItemInMarket>();
                Item.Initialize(i, pages[2]);
            }
        
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
            totalPrice += item.ItemSO.Price;
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

                if (inventory.Add(ItemInstance.Instantiate(itemToBuyInMarket.ItemSO)))
                {
                    inventoryCurrency.AddCrystal(-itemToBuyInMarket.ItemSO.Price/* * originCount*/);
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
