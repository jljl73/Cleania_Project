using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvasManager : MonoBehaviour
{
    private void Awake()
    {
        GameManager.Instance.MainCanvas = GetComponent<Canvas>();
    }
}
