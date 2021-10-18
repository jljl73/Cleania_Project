using System.Collections.Generic;

[System.Serializable]
public class SavedData_Inventory : iSavedData
{
    public List<ItemInstance> items = new List<ItemInstance>();

    void iSavedData.AfterLoad()
    {

    }

    void iSavedData.BeforeSave()
    {

    }
}
