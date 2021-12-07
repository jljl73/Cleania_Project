using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelect : MonoBehaviour
{
    public GameObject GenerateAskPanel;
    public GameObject FullPanel;
    public GameObject DeleteAskPanel;
    public GameObject DeletedPanel;

    public GameObject CharacterSlotParent;
    public GameObject Prefab_CharacterSlot;
    public Text CharacterNamePanel;

    string folderPath;
    string[] savedJsons;
    SavedData[] savedDatas;

    public string NextScene;
    // Start is called before the first frame update

    private void Awake()
    {
        folderPath = $"{Application.dataPath}/savedata/";
        savedJsons = new string[6];
        savedDatas = new SavedData[6];
    }

    void Start()
    {
        int i = 0;
        if (Directory.GetFiles(folderPath, "*.json").Length > 0)
        {
            foreach (string path in Directory.GetFiles(folderPath, "*.json"))
            { 
                if (i >= 6)
                    break;

                string name = path.Remove(0, folderPath.Length);
                name = name.Remove(name.Length - 5, 5);

                if (name.CompareTo("debug") == 0)
                    continue;

                savedDatas[i] = new SavedData();
                savedDatas[i].CharacterName = name;
                

                GameObject slot = Instantiate(Prefab_CharacterSlot, CharacterSlotParent.transform);
                UI_CharacterSlot slotComp = slot.GetComponent<UI_CharacterSlot>();
                slotComp.characterName.text = savedDatas[i].CharacterName;
                slotComp.characterLevel.text = savedDatas[i].PlayerExp.ToString();

                i++;
            }

            EventSystem.current.SetSelectedGameObject(CharacterSlotParent.transform.GetChild(0).gameObject);
            SavedData.Instance.CharacterName = savedDatas[0].CharacterName;
        }
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

        CharacterNamePanel.text = SavedData.Instance.CharacterName;
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
