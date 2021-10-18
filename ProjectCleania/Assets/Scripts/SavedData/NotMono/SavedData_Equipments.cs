using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SavedData_Equipments : iSavedData
{
    Equipable playerEquips;
    [SerializeField]
    string json;

    void iSavedData.AfterLoad()
    {
        playerEquips = GameManager.Instance.PlayerEquipments;

        // load
        JsonUtility.FromJsonOverwrite(json, playerEquips);

        ((iSavedData)playerEquips).AfterLoad();
    }

    void iSavedData.BeforeSave()
    {
        ((iSavedData)playerEquips).BeforeSave();
        
        // save
        json = JsonUtility.ToJson(playerEquips);
    }
}
