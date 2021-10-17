using System.Collections.Generic;

[System.Serializable]
public class SavedData_Inventory : iSavedData
{
    public List<ItemInstance> items = new List<ItemInstance>();

    public void AfterLoad()
    {

    }

    public void BeforeSave()
    {

    }
}
