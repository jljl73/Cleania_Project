using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    public Transform Pages;
    int index = 0;

    public void NextPage()
    {
        Pages.GetChild(index++).gameObject.SetActive(false);
        if (index == Pages.childCount) index = 0;
        Pages.GetChild(index).gameObject.SetActive(true);
    }

    void OnDisable()
    {
        Pages.GetChild(index).gameObject.SetActive(false);
        index = 0;
        Pages.GetChild(index).gameObject.SetActive(true);
    }
}
