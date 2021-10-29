using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCRepair : MonoBehaviour
{
    public GameObject slot;
    GameObject Image = null;
    GameObject selectedItem;

    public void SelectItem(GameObject item)
    {
        if (Image != null) Destroy(Image);
        selectedItem = item;
        // Image �ҷ����� ���� �ʿ��Ҽ���
        Image = Instantiate(selectedItem.transform.GetChild(0).gameObject, slot.transform);
    }

    public void RepairSelected()
    {

    }

    public void RepairAll()
    {

    }

    public void RepairEquipment()
    {

    }
}
