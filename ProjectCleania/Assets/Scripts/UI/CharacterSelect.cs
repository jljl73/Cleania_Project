using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CharacterSelect : MonoBehaviour
{
    [SerializeField]
    GameObject panels;

    public GameObject CharacterSlotParent;
    public GameObject Prefab_CharacterSlot;
    public Text CharacterNamePanel;

    public Text DeleteAskText;
    public Text DeleteDoneText;

    public InputField newCharacterName;

    string folderPath;
    //string[] savedJsons;
    SavedData[] savedDatas;

    public string NextScene;
    // Start is called before the first frame update

    private void Awake()
    {
        folderPath = $"{Application.dataPath}/savedata/";
        //savedJsons = new string[6];
        savedDatas = new SavedData[6];
    }

    void Start()
    {
        _RefreshCharacterSlotList();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseAllPanels();
        }

        if (SavedData.Instance.CharacterName.CompareTo("debug") != 0)
            CharacterNamePanel.text = SavedData.Instance.CharacterName;
        else
            CharacterNamePanel.text = "-";
    }   

    public void UpdateNamesInPanels()
    {
        if (SavedData.Instance.CharacterName.CompareTo("debug") != 0)
        {
            DeleteAskText.text = $"<size=11><color=#FF7F50>°æ°í!</color></size>\n¼±ÅÃÇÑ Ä³¸¯ÅÍ ÀÌ¸§: {SavedData.Instance.CharacterName}\n\nÄ³¸¯ÅÍ¸¦ »èÁ¦ÇÏ¸é<color=#FF7F50>´Ù½Ã º¹±¸ ÇÒ ¼ö ¾ø½À´Ï´Ù.</color>\nÁ¤¸»·Î ÀÌ Ä³¸¯ÅÍ¸¦<color=#FF7F50>»èÁ¦</color>ÇÏ½Ã°Ú½À´Ï±î?";
            DeleteDoneText.text = $"<size=10>¼±ÅÃÇÑ Ä³¸¯ÅÍ ÀÌ¸§: {SavedData.Instance.CharacterName}</size>\n\nÇØ´ç Ä³¸¯ÅÍ¸¦ »èÁ¦ÇÏ¿´½À´Ï´Ù.";
        }
        else
            CloseAllPanels();
    }

    public void CloseAllPanels()
    {
        for (int i = 0; i < panels.transform.childCount; ++i)
            panels.transform.GetChild(i).gameObject.SetActive(false);
    }

    public void OnClick_ButtonBack()
    {
        GameManager.Instance.ChangeScene("TitleScene");
    }

    public void OnClick_ButtonStart()
    {
        if (SavedData.Instance.CharacterName.CompareTo("debug") != 0)
            GameManager.Instance.ChangeScene(NextScene);
        else
        {
            CloseAllPanels();
            UI_MessageBox.Show("Ä³¸¯ÅÍ¸¦ ¼±ÅÃÇØÁÖ¼¼¿ä.");
        }
    }

    public void OnClick_ButtonDelete()
    {
        if (SavedData.Instance.CharacterName.CompareTo("debug") != 0)
        {
            //string currentName = SavedData.Instance.CharacterName;
            //SavedData.Instance.CharacterName = "debug";
            File.Delete(folderPath + SavedData.Instance.CharacterName + ".json");
            _RefreshCharacterSlotList();    // refresh + character name setting
        }
        else
        {
            CloseAllPanels();
            UI_MessageBox.Show("Ä³¸¯ÅÍ¸¦ ¼±ÅÃÇØÁÖ¼¼¿ä.");
        }
    }

    public void OnClick_ButtonGenerate()
    {
        if (CharacterSlotParent.transform.childCount >= 6)
        {
            CloseAllPanels();
            UI_MessageBox.Show("Ä³¸¯ÅÍ Ä­ÀÌ ²Ë Ã¡½À´Ï´Ù.\n±âÁ¸ Ä³¸¯ÅÍ¸¦ »èÁ¦ÇÑ µÚ ´Ù½Ã ½ÃµµÇØ ÁÖ¼¼¿ä.");
        }
        else
        {
            if (_CheckCharacterNameAvailable())
            {
                SavedData.Instance.GenerateFile(newCharacterName.text);
                _RefreshCharacterSlotList();
                CloseAllPanels();
            }
            else
            {
                CloseAllPanels();
                //UI_MessageBox.Show("»ç¿ëÇÒ ¼ö ¾ø´Â ÀÌ¸§ÀÔ´Ï´Ù.");
            }
        }
    }


    void _RefreshCharacterSlotList()
    {
        SavedData.Instance.CharacterName = "debug";

        GameObject[] slotsToDestroy = new GameObject[CharacterSlotParent.transform.childCount];
        for (int index = 0; index < slotsToDestroy.Length; index++)
            slotsToDestroy[index] = CharacterSlotParent.transform.GetChild(index).gameObject;
        for (int index = 0; index < slotsToDestroy.Length; index++)
            Destroy(slotsToDestroy[index]);

        for (int index = 0; index < savedDatas.Length; ++index)
            savedDatas[index] = null;

        int i = 0;
        if (Directory.GetFiles(folderPath, "*.json").Length > 1)    // debug.json is counted as 1
        {
            foreach (string path in Directory.GetFiles(folderPath, "*.json"))
            {
                if (i >= 6)
                    break;

                string name = _PathToName(path);

                if (name.CompareTo("debug") == 0)
                    continue;

                if (name != SavedData.Instance.CharacterName)
                {
                    savedDatas[i] = new SavedData();
                    savedDatas[i].CharacterName = name;
                }
                else
                    savedDatas[i] = SavedData.Instance;



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

    string _PathToName(string path)
    {
        string name = path.Remove(0, folderPath.Length);    // remove path
        name = name.Remove(name.Length - 5, 5);     // remove ".json"

        return name;
    }

    bool _CheckCharacterNameAvailable()
    {
        if (newCharacterName.text.Length == 0)
        {
            UI_MessageBox.Show("ÀÌ¸§ÀÌ ¾øÀ¸¸é ¾ÈµÅ¿ä..");
            return false;
        }
            if(newCharacterName.text.Length > 8)
        {
            UI_MessageBox.Show("ÀÌ¸§Àº ÃÖ´ë 8±ÛÀÚÀÔ´Ï´Ù.");
            return false;
        }

        if (CheckingSpecialText(newCharacterName.text))
        {
            UI_MessageBox.Show("ÀÌ¸§Àº ¿µ¾î, ÇÑ±Û, ¼ýÀÚ¸¸ Æ÷ÇÔµË´Ï´Ù.");
            return false;
        }

        if (newCharacterName.text.CompareTo("debug") == 0)
        {
            UI_MessageBox.Show("»ç¿ëÇÒ ¼ö ¾ø´Â ÀÌ¸§ÀÔ´Ï´Ù.");
            return false;
        }

        if (File.Exists(folderPath + newCharacterName.text + ".json"))
        {
            UI_MessageBox.Show("ÇØ´ç ÀÌ¸§Àº ÀÌ¹Ì Á¸ÀçÇÕ´Ï´Ù.");
            return false;
        }

        return true;
    }

    /// <summary>
    /// Æ¯¼ö¹®ÀÚ°¡ ÀÖ´ÂÁö È®ÀÎÇÏ±â À§ÇØ¼­ string Á¤±Ô½ÄÀ» ÀÌ¿ëÇÏ¿© Æ¯¼ö¹®ÀÚ¸¦ »« ¹®ÀÚ¸¦ ¹Þ´Â´Ù.       <para></para>
    /// ±×¸®°í ¿ø·¡ÀÇ string°ú ºñ±³ ÈÄ ´Ù¸£¸é true¸¦ ¸®ÅÏÇÑ´Ù.<para></para>
    /// from : https://plzhoney.tistory.com/21
    /// </summary>
    /// <param name="txt"></param>
    /// <returns></returns>
    public bool CheckingSpecialText(string txt)
    {
        string checker = Regex.Replace(newCharacterName.text, @"[^0-9a-zA-Z°¡-ÆR]", "", RegexOptions.Singleline);

        if (newCharacterName.text.Equals(checker) == false)
            return true;
        else
            return false;
    }
}
