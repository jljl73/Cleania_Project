using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinglePlayer : MonoBehaviour
{
    void Start()
    {
        GameManager.Instance.SinglePlayer = this.gameObject;
        GameManager.Instance.Equipments = GetComponent<EquipmentSlot>();
    }
}
