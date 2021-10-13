using System.Collections.Generic;

[System.Serializable]
public class SavedGame_Inventory : iSavedGame
{
    public List<ItemData> items = new List<ItemData>();

    public void AfterLoad()
    {

    }

    public void BeforeSave()
    {

    }
}
