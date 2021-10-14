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
        foreach(Equipment e in Equipments)
        {
            e.AfterLoad();

            playerEquips.Equip(e);
        }

        //Equipments.Clear();
    }

    public void BeforeSave()
    {
        Equipments.Clear();

        for (Equipment.Type i = Equipment.Type.MainWeapon; i < Equipment.Type.EnumTotal; i++)
        {
            Equipment e = playerEquips.Unequip(i);

            if (e != null)
            {
                e.BeforeSave();
                Equipments.Add(e);
                playerEquips.Equip(e);
            }
        }
    }
}
