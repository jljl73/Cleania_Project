using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogSelector : MonoBehaviour
{
    [SerializeField]
    public Quest quest;

    public void ShowDialog()
    {
        int index = 0;
        if (quest != null)
            index = (int)quest.State;

        transform.GetChild(index).gameObject.SetActive(true);
    }

}