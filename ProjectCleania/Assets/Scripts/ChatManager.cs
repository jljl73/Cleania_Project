using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;


public class ChatManager : MonoBehaviour
{
    public InputField inputField;
    public ScrollRect scrollRect;
    public GameObject settingPopup;
    public Text chatText;
    public Toggle[] toggles;
    // Start is called before the first frame update

    struct MSG
    {
        public int type;
        public string msg;
        public MSG(int type, string msg)
        {
            this.type = type;
            this.msg = msg;
        }
    }

    List<MSG> messages = new List<MSG>();
    
    private void Start()
    {
        GameManager.Instance.chatManager = this;
        ShowAcquireClean(321);
        ShowAcquireExp(3215);
        ShowAcquireItem("dsfdsf");
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            inputField.Select();
    }

    public void OnClickedOK()
    {
        settingPopup.SetActive(false);
    }

    public void OnClickedUp()
    {
        scrollRect.content.Translate(-scrollRect.content.up * 10);
    }

    public void OnClickedDown()
    {
        if(scrollRect.content.anchoredPosition.y < 0)
            scrollRect.content.Translate(scrollRect.content.up * 10);
    }

    public void OnClicekdDD()
    {
        scrollRect.content.anchoredPosition = Vector3.zero;
    }

    public void OnClickedSetting()
    {
        settingPopup.SetActive(!settingPopup.activeSelf);
    }

    public void ShowSystemMessage(string msg)
    {
        messages.Add(new MSG(0, string.Format("\n<color=#FF0000>{0}</color>", msg)));
        UpdateChat();
    }

    public void OnSubmitChat()
    {
        messages.Add(new MSG(3, "\n[player]" + inputField.text));
        UpdateChat();
        inputField.text = "";
    }

    public void ShowAcquireClean(int n)
    {
        messages.Add(new MSG(1, string.Format("\n<color=#0000CD>{0} Ŭ�� ȹ��</color>", n)));
        UpdateChat();
    }

    public void ShowAcquireExp(int n)
    {
        messages.Add(new MSG(4, string.Format("\n<color=#0000CD>{0} ����ġ ȹ��</color>", n)));
        UpdateChat();
    }


    public void ShowAcquireItem(string name)
    {
        // ������������ ���߿�
        messages.Add(new MSG(2, "\n<color=#FFD700>[Pleyer] ���� [" + name + "] ����!</color>"));
        UpdateChat();
    }

    
    public void UpdateChat()
    {
        StringBuilder sb = new StringBuilder();
        foreach(MSG m in messages)
        {
            if(toggles[m.type].isOn)
                sb.Append(m.msg);
        }

        chatText.text = sb.ToString();
    }

}
