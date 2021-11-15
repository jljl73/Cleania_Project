using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class QuestDB : MonoBehaviour
{




    public static void Save(Quest quest)
    {
        Debug.Log(JsonUtility.ToJson(quest));
    }

    private void Update()
    {
        
    }
}
