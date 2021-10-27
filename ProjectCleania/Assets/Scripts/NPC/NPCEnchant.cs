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
    GameObject selectedItem;
    ItemInstance instance;

    public void SelectItem(GameObject item)
    {
        if (Image != null) Destroy(Image);
        selectedItem = item;
        // Image 불러오기 수정 필요할수도
        Image = Instantiate(selectedItem.transform.GetChild(0).gameObject, ItemImage.transform);
        instance = item.GetComponent<ItemController_v2>().itemInstance;

        ItemName.text = instance.SO.ItemName;
        ItemDetail.text = instance.SO.ToolTip;
        
        for(int i = 0; i < instance.SO.OptionTable.DynamicTable.Length; ++i)
        {
            options[i].SetActive(true);
            options[i].transform.GetComponentInChildren<Text>().text = instance.SO.OptionTable.DynamicTable[i].KeyStat.ToString();
        }

    }

}
