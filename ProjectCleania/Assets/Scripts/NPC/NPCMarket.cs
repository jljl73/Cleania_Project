using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCMarket : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public GameObject[] pages;
    List<GameObject> items = new List<GameObject>();

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
    
}
