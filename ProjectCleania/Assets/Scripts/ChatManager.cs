using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class ChatManager : MonoBehaviour
{
    public InputField inputField;
    public ScrollRect scrollRect;
    public Text chatText;
    // Start is called before the first frame update

    private void Start()
    {
        ShowAcquireClean(321);
        ShowAcquireExp(3215);
        ShowAcquireItem("dsfdsf");
    }


    public void OnClickedUp()
    {
        scrollRect.content.Translate(-scrollRect.content.up * 10);
    }

    public void OnClickedDown()
    {
        scrollRect.content.Translate(scrollRect.content.up * 10);
    }

    public void OnClicekdDD()
    {
        scrollRect.content.localPosition = Vector3.zero;
    }

    public void OnSubmitChat()
    {
        chatText.text += "\n" + "[Player] " + inputField.text;
        inputField.text = "";
    }

    public void ShowAcquireItem(string name)
    {
        StringBuilder sb = new StringBuilder("\n<color=#FFD700>[Pleyer] ¥‘¿Ã [");
        sb.Append(name);
        sb.Append("] Ω¿µÊ!</color>");
        chatText.text += sb.ToString();
    }

    public void ShowAcquireExp(int n)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("\n<color=#0000CD>{0} ∞Ê«Ëƒ° »πµÊ</color>", n);
        chatText.text += sb.ToString();
    }

    public void ShowAcquireClean(int n)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendFormat("\n<color=#0000CD>{0} ≈¨∏∞ »πµÊ</color>", n);
        chatText.text += sb.ToString();
    }
}
