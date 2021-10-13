

[System.Serializable]
public class SavedGame_Equipments : iSavedGame
{
    public Equipment[] Equipments = new Equipment[(int)Equipment.Type.EnumTotal];
    [System.NonSerialized]
    public Equipable playerEquips;

    public void AfterLoad()
    {
        for (Equipment.Type i = Equipment.Type.MainWeapon; i < Equipment.Type.EnumTotal; i++)
        {
            //if (Equipments[(int)i] != null)
            playerEquips.Equip(Equipments[(int)i]);
        }
    }

    public void BeforeSave()
    {
        for (Equipment.Type i = Equipment.Type.MainWeapon; i < Equipment.Type.EnumTotal; i++)
        {
            Equipments[(int)i] = playerEquips.Unequip(i);
        }
    }
}
