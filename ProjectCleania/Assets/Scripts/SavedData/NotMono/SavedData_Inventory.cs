using System.Collections.Generic;

[System.Serializable]
public class SavedData_Inventory : iSavedData
{
    public List<ItemData> items = new List<ItemData>();

    public void AfterLoad()
    {

    }

    public void BeforeSave()
    {

    }
}
