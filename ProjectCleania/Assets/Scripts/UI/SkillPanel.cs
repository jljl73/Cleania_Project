using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    public GameObject[] pages;
    public Text title;
    // Start is called before the first frame update
    void Start()
    {
        ShowMousePage();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowMousePage()
    {
        title.text = "���콺 ��ų";
        pages[1].SetActive(true);
        pages[0].SetActive(false);
    }

    public void ShowKeyboardPage()
    {
        title.text = "Ű���� ��ų";
        pages[1].SetActive(false);
        pages[0].SetActive(true);
    }

}
