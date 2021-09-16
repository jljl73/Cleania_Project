using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryItemGenerator : MonoBehaviour
{
    public GameObject _iventory;
    public GameObject _clicked;
    public Canvas _canvas;
    public GraphicRaycaster _raycaster;
    public ItemInventory itemInventory;

    private void Start()
    {
        //itemInventory = GetComponent<ItemInventory>();
    }

    //юс╫ц
    public GameObject prefab;

    public void GenerateItem()
    {
        GameObject newItem = Instantiate(prefab);
        newItem.GetComponent<ItemController>().PutInventory(itemInventory, _iventory, _clicked, _canvas, _raycaster);
        newItem.SetActive(true);
    }
}
