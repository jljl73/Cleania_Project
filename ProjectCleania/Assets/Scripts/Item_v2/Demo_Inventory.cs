using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Demo_Inventory : MonoBehaviour
{
    Grid[] slots;
    Image[] images;
    ItemStorage_LocalGrid inven;


    // Start is called before the first frame update
    void Start()
    {
        slots = GetComponentsInChildren<Grid>();
        images = GetComponentsInChildren<Image>();
        inven = SavedData.Instance.Item_Inventory;
    }

    // Update is called once per frame
    void Update()
    {
        foreach(var i in images)
        {
            i.enabled = false;
        }

        foreach(var i in inven.Items)
        {
            images[i.Value.Y * 10 + i.Value.X].enabled = true;
            images[i.Value.Y * 10 + i.Value.X].sprite = i.Key.SO.ItemImage;
        }
    }
}
