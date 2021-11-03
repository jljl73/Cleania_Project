using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Dialog : MonoBehaviour
{
    Transform pages;
    int index = 0;

    void Awake()
    {
        pages = transform.GetChild(transform.childCount - 1);
        pages.GetChild(0).gameObject.SetActive(true);
    }

    public void NextPage(int nextIdx)
    {
        pages.GetChild(index).gameObject.SetActive(false);
        pages.GetChild(nextIdx).gameObject.SetActive(true);
        index = nextIdx;
    }


    void OnEnable()
    {
        NextPage(0);
    }
}
