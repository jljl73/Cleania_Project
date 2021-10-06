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
    }
}
