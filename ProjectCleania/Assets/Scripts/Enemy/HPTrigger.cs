using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HPTrigger : MonoBehaviour
{
    public float standardHP;
    public AbilityStatus abilityStatus;
    UnityEvent gameEvent = new UnityEvent();
    public int times = 1;

    public void RegisterListener(UnityAction action)
    {
        gameEvent.AddListener(action);
    }

    public void UnregisterListener(UnityAction action)
    {
        gameEvent.RemoveListener(action);
    }

    public void Invoke()
    {
        if(times-- > 0)
            gameEvent.Invoke();
    }

    void Update()
    {
        if (abilityStatus.HP <= standardHP)
        {
            Invoke();
        }
    }
}
