using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCEnchant : MonoBehaviour
{
    GameObject selectedItem;

    public void SelectItem(GameObject item)
    {
        selectedItem = item;
    }
}
