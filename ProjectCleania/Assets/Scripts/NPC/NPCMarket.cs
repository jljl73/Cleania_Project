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

    List<ItemInMarket> items = new List<ItemInMarket>();
    Queue<ItemInMarket> soldItems = new Queue<ItemInMarket>();

    private void Awake()
    {
        for (int i = 0; i < 5; ++i)
        {
            ItemInMarket newItem = Instantiate(prefab_Item, pages[0].transform).GetComponent<ItemInMarket>();
            newItem.Initialize(ItemInstance.Instantiate_RandomByRank(ItemSO.enumRank.Common), pages[0]);
            ItemInMarket newItem2 = Instantiate(prefab_Item, pages[1].transform).GetComponent<ItemInMarket>();
            newItem2.Initialize(ItemInstance.Instantiate_RandomByRank(ItemSO.enumRank.Common), pages[1]);
        }

        toggleGroup = pages[0];
        ShowPage(0);
    }


    public void ShowPage(int index)
    {
        for(int i = 0; i < pages.Length; ++i)
            pages[i].gameObject.SetActive(false);

        pages[index].gameObject.SetActive(true);
        toggleGroup = pages[index];
        pageScroll.content = pages[index].GetComponent<RectTransform>();
    }

    public void Buy()
    {
        var toggles = toggleGroup.ActiveToggles();
        int totalPrice = 0;
        items.Clear();

        foreach(Toggle t in toggles)
        {
            // ����
            var item = t.GetComponent<ItemInMarket>();
            items.Add(item);
            totalPrice += item.itemInstance.SO.Price * item.itemInstance.Count;
        }

        UI_Currency inventoryCurrency = GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_Currency>();
        if(inventoryCurrency.Crystal < totalPrice)
        {
            UI_MessageBox.Message("Ŭ���� �����մϴ�.");
            return;
        }
        else
        {
            UI_ItemContainer inventory = inventoryCurrency.GetComponent<UI_ItemContainer>();
            foreach(var i in items)
            {
                UI_ItemController newController = UI_ItemController.New(i.itemInstance);

                if (inventory.Add(newController))
                {
                    inventoryCurrency.AddCrystal(-newController.itemInstance.SO.Price * newController.itemInstance.Count);
                    Destroy(i.gameObject);
                }
                else
                {
                    UI_ItemController.Delete(newController);
                    UI_MessageBox.Message("�κ��丮�� �� á���ϴ�.");
                    break;
                }

            }
            items.Clear();
        }
        
    }

    public void SellItem(UI_ItemController controller)
    {
        ShowPage(2);
        ItemInMarket newItem = Instantiate(prefab_Item, pages[2].transform).GetComponent<ItemInMarket>();
        newItem.Initialize(controller.itemInstance, pages[2]);
        soldItems.Enqueue(newItem);

        GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_Currency>().AddCrystal(controller.itemInstance.SO.Price);
        GameManager.Instance.uiManager.InventoryPanel.GetComponent<UI_ItemContainer>().Remove(controller);
        UI_ItemController.Delete(controller);

        if (soldItems.Count >= 10)
            Destroy(soldItems.Dequeue().gameObject);
    }
    
}
