using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class EnemyEvent : MonoBehaviour
{
    UnityEvent enemyEvent = new UnityEvent();

    public void RegisterListener(UnityAction action)
    {
        enemyEvent.AddListener(action);
    }

    public void UnregisterListener(UnityAction action)
    {
        enemyEvent.RemoveListener(action);
    }

    public void Invoke()
    {
        enemyEvent.Invoke();
    }
}
