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

    public void OnOffPanel()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void ShowMousePage()
    {
        title.text = "마우스 스킬";
        pages[1].SetActive(true);
        pages[0].SetActive(false);
    }

    public void ShowKeyboardPage()
    {
        title.text = "키보드 스킬";
        pages[1].SetActive(false);
        pages[0].SetActive(true);
    }

}
