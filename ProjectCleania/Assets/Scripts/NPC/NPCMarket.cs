using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCMarket : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public GameObject[] pages;
    public GameObject prefab_Item;

    List<ItemInMarket> items = new List<ItemInMarket>();
    Queue<ItemInMarket> soldItems = new Queue<ItemInMarket>();

    private void Awake()
    {
        for (int i = 0; i < 5; ++i)
        {
            ItemInMarket newItem = Instantiate(prefab_Item, pages[0].transform).GetComponent<ItemInMarket>();
            newItem.Initialize(ItemInstance.Instantiate_RandomByRank(ItemSO.enumRank.Common));
            ItemInMarket newItem2 = Instantiate(prefab_Item, pages[1].transform).GetComponent<ItemInMarket>();
            newItem2.Initialize(ItemInstance.Instantiate_RandomByRank(ItemSO.enumRank.Common));
        }

        toggleGroup = pages[0].GetComponent<ToggleGroup>();
        ShowPage(0);
    }


    public void ShowPage(int index)
    {
        for(int i = 0; i < pages.Length; ++i)
            pages[i].SetActive(false);

        pages[index].SetActive(true);
        toggleGroup = pages[index].GetComponent<ToggleGroup>();
    }

    public void Buy()
    {
        var toggles = toggleGroup.ActiveToggles();
        int totalPrice = 0;
        items.Clear();

        foreach(Toggle t in toggles)
        {
            // 구매
            var item = t.GetComponent<ItemInMarket>();
            items.Add(item);
            totalPrice += item.itemInstance.SO.Price * item.itemInstance.Count;
        }

        Storage inventory = GameManager.Instance.uiManager.InventoryPanel.GetComponent<Storage>();
        if(inventory.Crystal < totalPrice)
        {
            UI_MessageBox.Message("클린이 부족합니다.");
            return;
        }
        else
        {
            foreach(var i in items)
            {
                ItemController_v2 newController = ItemController_v2.New(i.itemInstance, inventory);
                inventory.Add(newController, out newController.prevIndex);

                if (newController.prevIndex != -1)
                {
                    inventory.AddCrystal(-newController.itemInstance.SO.Price * newController.itemInstance.Count);
                    Destroy(i.gameObject);
                }
                else
                {
                    ItemController_v2.Delete(newController);
                    UI_MessageBox.Message("인벤토리가 꽉 찼습니다.");
                    break;
                }

            }
            items.Clear();
        }
        
    }

    public void SelectItem(ItemInMarket item)
    {
        items.Add(item);
    }

    public void SellItem(ItemController_v2 controller)
    {
        ShowPage(2);
        ItemInMarket newItem = Instantiate(prefab_Item, pages[2].transform).GetComponent<ItemInMarket>();
        newItem.Initialize(controller.itemInstance);
        soldItems.Enqueue(newItem);

        GameManager.Instance.uiManager.InventoryPanel.GetComponent<Storage>().AddCrystal(controller.itemInstance.SO.Price);
        ItemController_v2.Delete(controller);

        if (soldItems.Count >= 10)
            Destroy(soldItems.Dequeue().gameObject);
    }
    
}
