using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.SinglePlayer = this.gameObject;
        GameManager.Instance.PlayerAbility = GetComponent<AbilityStatus>();
        GameManager.Instance.PlayerStatus = GetComponent<Status>();
        GameManager.Instance.PlayerEquipments = GetComponent<EquipmentSlot>();
    }
}
