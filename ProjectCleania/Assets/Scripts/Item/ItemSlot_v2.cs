using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSlot_v2 : MonoBehaviour
{
    public int Index { get; private set; }
    public bool IsActive;

    void Start()
    {
        Index = transform.GetSiblingIndex();
    }

}
