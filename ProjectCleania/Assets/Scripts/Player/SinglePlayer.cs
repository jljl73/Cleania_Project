using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer : MonoBehaviour
{
    void Awake()
    {
        GameManager.Instance.SinglePlayer = this.gameObject;
        GameManager.Instance.PlayerAbility = GetComponentInChildren<AbilityStatus>();
        GameManager.Instance.PlayerStatus = GetComponentInChildren<Status>();
        GameManager.Instance.PlayerEquipments = GetComponentInChildren<Equipable>();
        GameManager.Instance.PlayerBuffs = GetComponentInChildren<Buffable>();
        GameManager.Instance.player = GetComponent<Player>();
    }
}
