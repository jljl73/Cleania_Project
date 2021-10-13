using System.Collections.Generic;

[System.Serializable]
public class SavedGame_Equipments : iSavedGame
{
    [UnityEngine.SerializeField]
    List<Equipment> Equipments = new List<Equipment>();
    [System.NonSerialized]
    public Equipable playerEquips;

    public void AfterLoad()
    {
        foreach(var e in Equipments)
        {
            //if (Equipments[(int)i] != null)
            playerEquips.Equip(e);
        }
    }

    public void BeforeSave()
    {
        for (Equipment.Type i = Equipment.Type.MainWeapon; i < Equipment.Type.EnumTotal; i++)
        {
            var e = playerEquips.Unequip(i);

            if (e != null)
            {
                Equipments.Add(e);
                playerEquips.Equip(e);
            }
        }
    }
}
