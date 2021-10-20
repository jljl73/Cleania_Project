using System.Collections.Generic;
using UnityEngine;
using System.Drawing;

[System.Serializable]
public class SavedData_Inventory : iSavedData
{
    [SerializeField]
    public ItemStorage_LocalGrid inventory = new ItemStorage_LocalGrid(new Size(10, 6));


    void iSavedData.AfterLoad()
    {
        ((iSavedData)inventory).AfterLoad();
    }

    void iSavedData.BeforeSave()
    {
        ((iSavedData)inventory).BeforeSave();
    }
}
