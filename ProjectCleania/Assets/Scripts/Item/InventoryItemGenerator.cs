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
    public GameObject _player;
    public Json json;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L))
        {
            DropItem("1101001");
        }
    }

    //임시
    public GameObject prefab;

    // 오브젝트 생성
    public void DropItem(string itemID)
    {
        GameObject newItem = Instantiate(Resources.Load("Prefabs/" + itemID, typeof(GameObject)) as GameObject, _player.transform.position, _player.transform.rotation);

    }

    public void DropItem(Item item)
    {
        string ObjectName = item.ItemID.ToString();

        GameObject newItem = Instantiate(Resources.Load("Prefabs/" + ObjectName, typeof(GameObject)) as GameObject, _player.transform.position, _player.transform.rotation);

        GameObject.Find("ItemList").GetComponent<ItemList>().AddToField(item);
        newItem.GetComponent<ItemObject>().CopyItem(item);
    }

    // 컨트롤러 생성
    public void GenerateItem(Item item)
    {
        string ObjectName = item.ItemCode.ToString() + "_inven";

        GameObject newItem = Instantiate(Resources.Load("Prefabs/Items/" + ObjectName, typeof(GameObject)) as GameObject, transform.position, transform.rotation);

        newItem.GetComponent<ItemController>().Initialize(item);
        newItem.SetActive(true);
    }
}
