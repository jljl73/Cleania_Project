using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestPanel : MonoBehaviour
{
    [SerializeField]
    Sprite spritePlus;
    [SerializeField]
    Sprite spriteMinus;
    public Button[] buttons;
    public GameObject[] details;

    public List<GameObject> TextPrefabs;

    Vector2 sizeDelta = new Vector2(0, 0);
    
    GameObject EmptyPrefab
    {
        get
        {
            for (int i = 0; i < TextPrefabs.Count; ++i)
            {
                if (!TextPrefabs[i].activeSelf)
                    return TextPrefabs[i];
            }

            GameObject newObject = Instantiate(TextPrefabs[0]);
            TextPrefabs.Add(newObject);
            return newObject;
        }
    }

    QuestManager QM { get => GameManager.Instance.uiManager.GetComponent<QuestManager>(); }
    
    void Start()
    {
        QM.AddListener_Update(UpdateList);

        for (int i = 0; i < buttons.Length; ++i)
        {
            int temp = i;
            buttons[i].onClick.AddListener(() => ShowDetails(temp));
        }

        sizeDelta = TextPrefabs[0].GetComponent<RectTransform>().sizeDelta;
    }

    public void UpdateList()
    {
        for (int i = 0; i < TextPrefabs.Count; ++i)
            TextPrefabs[i].SetActive(false);

        for (int i = 0; i < QM.Quests.Count; ++i)
        {
            Quest quest = QM.Quests[i];
            int index = (int)quest.Catergory;

            if (quest.State == Quest.STATE.Assign || quest.State == Quest.STATE.Clear)
            {
                GameObject newText = EmptyPrefab;

                newText.SetActive(true);
                newText.transform.SetParent(details[index].transform);
                newText.GetComponent<TextMeshProUGUI>().text = quest.Name;
                newText.GetComponent<QuestSlot>().Initialize(quest);
            }
        }
    }

    public void ShowDetails(int index)
    {
        for (int i = 0; i < details.Length; ++i)
        {
            details[i].SetActive(false);
            buttons[i].GetComponent<Image>().sprite = spritePlus;
        }

        buttons[index].GetComponent<Image>().sprite = spriteMinus;

        details[index].GetComponent<RectTransform>().sizeDelta =
            new Vector2(sizeDelta.x, details[index].transform.childCount * sizeDelta.y);
        details[index].SetActive(true);
    }

}
