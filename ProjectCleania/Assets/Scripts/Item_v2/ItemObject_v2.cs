using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class ItemObject_v2 : MonoBehaviour 
{
    #region ObjectPool

    static GameObject ItemObjectPrefab;
    static List<GameObject> _objectPool = new List<GameObject>();

    static public ItemObject_v2 New(ItemInstance item, Vector3 position, Quaternion rotation)
    {
        GameObject newObject;

        if (_objectPool.Count > 0)
        {
            // use object in pool
            newObject = _objectPool[0]; _objectPool.RemoveAt(0);    //dequeue
            SceneManager.MoveGameObjectToScene(newObject, SceneManager.GetActiveScene());
        }
        else
        {
            if(ItemObjectPrefab == null)
                ItemObjectPrefab = Resources.Load<GameObject>("Prefabs/ItemObject");

            // new object
            newObject = GameObject.Instantiate(ItemObjectPrefab);
        }

        newObject.SetActive(true);
        newObject.transform.position = position;
        newObject.transform.rotation = rotation;

        ItemObject_v2 container = newObject.GetComponent<ItemObject_v2>();
        container.ItemData = item;

        return container;
    }

    static public void Delete(GameObject removingObject)
    {
        // back to object pool
        _objectPool.Add(removingObject);

        removingObject.SetActive(false);
        DontDestroyOnLoad(removingObject);

        removingObject.GetComponent<ItemObject_v2>().ItemData = null;
    }

    #endregion

    private ItemInstance itemData;
    public ItemInstance ItemData
    {
        get => itemData;
        set
        {
            _SetItem(value);
        }
    }

    private void OnDestroy()
    {
        itemData?.CurrentStorage.Remove(itemData);
        _objectPool.Remove(gameObject);
    }

    void _SetItem(ItemInstance item)
    {
        itemData = item;

        if (item != null)
        {
            if (transform.childCount > 0)
                foreach (Transform child in transform)
                    GameObject.Destroy(child.gameObject);

            switch (item.SO.MainCategory)
            {
                case ItemSO.enumMainCategory.Equipment:
                    if (item.SO.SubCategory == ItemSO.enumSubCategory.MainWeapon)
                        GameObject.Instantiate(Resources.Load<GameObject>("External/Cleaning Clutter/Models/dustpan_01"), transform);
                    else
                        GameObject.Instantiate(Resources.Load<GameObject>("External/Cleaning Clutter/Models/bucket_01"), transform);
                    break;
                case ItemSO.enumMainCategory.Etc:
                    GameObject.Instantiate(Resources.Load<GameObject>("External/Cleaning Clutter/Models/trash_bags_01"), transform);
                    break;
            }
        }
    }

    //public void PickUp(GameObject item)
    //{

    //    ItemObject_v2 container = item.GetComponent<ItemObject_v2>();
    //    ItemInstance itemData = container.ItemData;

    //    if (SavedData.Instance.Item_Inventory.Add(itemData))
    //    {
    //        container.Parent.Remove(itemData);
    //        droppedItems.Remove(item);
    //    }
    //}

}
