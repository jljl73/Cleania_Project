using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScaleTest : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            gameObject.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
