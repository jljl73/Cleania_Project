using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelect : MonoBehaviour
{
    public GameObject GenerateAskPanel;
    public GameObject FullPanel;
    public GameObject DeleteAskPanel;
    public GameObject DeletedPanel;

    public string NextScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GenerateAskPanel.SetActive(false);
            FullPanel.SetActive(false);
            DeleteAskPanel.SetActive(false);
            DeletedPanel.SetActive(false);
        }
    }   

    public void Toggle_Generate()
    {
        GenerateAskPanel.SetActive(!GenerateAskPanel.activeSelf);
        if (GenerateAskPanel.activeSelf)
        {
            //GenerateAskPanel.SetActive(false);
            FullPanel.SetActive(false);
            DeleteAskPanel.SetActive(false);
            DeletedPanel.SetActive(false);
        }
    }
    public void Toggle_Full()
    {
        FullPanel.SetActive(!FullPanel.activeSelf);
        if (FullPanel.activeSelf)
        {
            GenerateAskPanel.SetActive(false);
            //FullPanel.SetActive(false);
            DeleteAskPanel.SetActive(false);
            DeletedPanel.SetActive(false);
        }
    }
    public void Toggle_Delete()
    {
        DeleteAskPanel.SetActive(!DeleteAskPanel.activeSelf);
        if (DeleteAskPanel.activeSelf)
        {
            GenerateAskPanel.SetActive(false);
            FullPanel.SetActive(false);
            //DeleteAskPanel.SetActive(false);
            DeletedPanel.SetActive(false);
        }
    }
    public void Toggle_Deleted()
    {
        DeletedPanel.SetActive(!DeletedPanel.activeSelf);
        if (DeletedPanel.activeSelf)
        {
            GenerateAskPanel.SetActive(false);
            FullPanel.SetActive(false);
            DeleteAskPanel.SetActive(false);
            //DeletedPanel.SetActive(false);
        }
    }

    public void OnClick_ButtonBack()
    {
        GameManager.Instance.ChangeScene("TitleScene");
    }

    public void OnClick_ButtonStart()
    {
        GameManager.Instance.ChangeScene(NextScene);
    }
}
