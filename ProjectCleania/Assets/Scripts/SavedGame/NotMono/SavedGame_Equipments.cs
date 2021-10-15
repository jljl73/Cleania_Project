using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedGame_Equipments : iSavedGame
{
    Equipable playerEquips;
    [SerializeField]
    string json;

    public void AfterLoad()
    {
        playerEquips = GameManager.Instance.PlayerEquipments;

        // load
        JsonUtility.FromJsonOverwrite(json, playerEquips);

        playerEquips.AfterLoad();
    }

    public void BeforeSave()
    {
        playerEquips.BeforeSave();
        
        // save
        json = JsonUtility.ToJson(playerEquips);
    }
}
