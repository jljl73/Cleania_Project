using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NPCEnchant : MonoBehaviour
{
    public GameObject ItemImage;
    public Text ItemName;
    public Text ItemDetail;
    public GameObject[] options;

    GameObject Image = null;
    ItemController_v2 selectedItem;
    ItemInstance instance;

    public void SelectItem(ItemController_v2 item)
    {
        if (Image != null) Destroy(Image);
        selectedItem = item;
        // Image 불러오기 수정 필요할수도
        Image = Instantiate(selectedItem.transform.GetChild(0).gameObject, ItemImage.transform);

        instance = item.GetComponent<ItemController_v2>().itemInstance;
        ItemName.text = instance.SO.ItemName;
        ItemDetail.text = instance.SO.ToolTip;

        ItemInstance_Equipment ie = (ItemInstance_Equipment)instance;

        int ct = 0;
        foreach(var v in ie.DynamicProperties_ToString())
        {
            options[ct].SetActive(true);
            options[ct++].transform.GetComponentInChildren<Text>().text = v;
        }

    }

    // public Change Option
    //
    // 

}
