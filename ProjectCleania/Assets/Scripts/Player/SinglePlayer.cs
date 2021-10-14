using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.SinglePlayer = this.gameObject;
        GameManager.Instance.PlayerAbility = GetComponent<AbilityStatus>();
        GameManager.Instance.PlayerStatus = GetComponent<Status>();
        GameManager.Instance.PlayerEquipments = GetComponent<EquipmentSlot>();
        GameManager.Instance.PlayerBuffs = GetComponent<BuffManager>();
        GameManager.Instance.player = GetComponent<Player>();

        if (GameManager.Instance.player == null)
            print("player doesnt exist im in singlePlayer");
        else
            print("player exist im in singlePlayer");
    }
}
