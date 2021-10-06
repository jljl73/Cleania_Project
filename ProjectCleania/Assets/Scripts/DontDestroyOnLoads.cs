using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoads : MonoBehaviour
{
    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
