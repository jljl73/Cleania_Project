using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour
{
    [System.Serializable]
    public struct fButton
    {
        public DialogButton.TYPE type;
        public string content;
        public string value;
    }

    [System.Serializable]
    public struct Page
    {
        [TextArea]
        public string content;
        public fButton[] buttons;
    }

    [SerializeField]
    Quest quest;
    public Page[] pages;
    public Transform PageTransform;

    private void OnEnable()
    {
        quest = transform.parent.GetComponent<DialogSelector>().quest;
        ChangePage(0);
    }

    public void ChangePage(int index)
    {
        //Debug.Log(index.ToString() + "Page");
        PageTransform.GetChild(0).GetComponent<TextMeshProUGUI>().text = pages[index].content;

        int b = 0;
        int length = pages[index].buttons.Length;
        for (; b < length; ++b)
        {
            DialogButton button = PageTransform.GetChild(1).GetChild(length - b - 1).GetComponent<DialogButton>();
            button.Initialize(pages[index].buttons[b].type, quest, pages[index].buttons[b].value);
            button.GetComponentInChildren<TextMeshProUGUI>().text = pages[index].buttons[b].content;
            button.gameObject.SetActive(true);
        }

        for(; b < PageTransform.GetChild(1).childCount; ++b)
        {
            PageTransform.GetChild(1).GetChild(b).gameObject.SetActive(false);
        }
    }
}
