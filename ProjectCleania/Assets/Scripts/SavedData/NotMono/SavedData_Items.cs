using System.Collections.Generic;
using UnityEngine;
using System.Drawing;

[System.Serializable]
public class SavedData_Items : iSavedData
{
    public ItemStorage_World World = new ItemStorage_World();
    public ItemStorage_LocalGrid Inventory = new ItemStorage_LocalGrid(new System.Drawing.Size(10, 6));
    public ItemStorage_LocalGrid Storage = new ItemStorage_LocalGrid(new System.Drawing.Size(10, 10));

    public void AfterLoad()
    {
        ((iSavedData)World).AfterLoad();
        ((iSavedData)Inventory).AfterLoad();
        ((iSavedData)Storage).AfterLoad();
    }

    public void BeforeSave()
    {
        ((iSavedData)World).BeforeSave();
        ((iSavedData)Inventory).BeforeSave();
        ((iSavedData)Storage).BeforeSave();
    }
}
