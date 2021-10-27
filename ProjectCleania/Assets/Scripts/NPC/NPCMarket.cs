using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCMarket : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public GameObject[] pages;
    public GameObject prefab_Item;

    List<GameObject> items = new List<GameObject>();
    Queue<GameObject> sellItems = new Queue<GameObject>();
    

    public void ShowPage(int index)
    {
        for(int i = 0; i < pages.Length; ++i)
            pages[i].SetActive(false);
        pages[index].SetActive(true);
    }

    public void Buy()
    {
        var toggles = toggleGroup.ActiveToggles();

        foreach(Toggle t in toggles)
        {
            Debug.Log(t.name);
        }
    }

    public void SelectItem(GameObject item)
    {
        items.Add(item);
    }

    public void SellItem(GameObject item)
    {
        ShowPage(2);
        GameObject newItem = Instantiate(prefab_Item, pages[2].transform);
        newItem.GetComponent<ItemInMarket>().Initialize(item.GetComponent<ItemController_v2>().itemInstance);
        sellItems.Enqueue(newItem);

        if (sellItems.Count >= 10)
            Destroy(sellItems.Dequeue());
    }
    
}
