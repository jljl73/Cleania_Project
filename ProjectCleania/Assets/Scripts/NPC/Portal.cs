using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField]
    string SceneName;
    void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.ChangeScene(SceneName);
    }
}
