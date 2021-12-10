using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//[System.Runtime.InteropServices.ComVisible(true)]
public enum DialogResult
{
    None = 0,
    OK = 1,
    Cancel = 2,
    //Abort = 3,
    //Retry = 4,
    //Ignore = 5,
    //Yes = 6,
    //No = 7
}

public enum MessageBoxButtons
{
    OK = 0,
    OKCancel = 1,
    //AbortRetryIgnore = 2,
    //YesNoCancel = 3,
    //YesNo = 4,
    //RetryCancel = 5
}

public class UI_MessageBox : MonoBehaviour
{
    [SerializeField]
    Text text;
    [SerializeField]
    Button buttonOK;
    [SerializeField]
    Button buttonCancel;

    DialogResult result = DialogResult.None;
    static public DialogResult Result
    { get => Instance.result; }
    //MessageBoxButtons buttons;

    static UI_MessageBox _singleton;
    static public UI_MessageBox Instance
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = ((GameObject)Instantiate(Resources.Load("Prefabs/UI_MessageBox")))
                    .GetComponent<UI_MessageBox>();

                _singleton.gameObject.SetActive(false);
            }
            return _singleton;
        }
    }


    static public void Show(string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
    {
        Instance.InstanceShow(message, buttons);
    }

    void InstanceShow(string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
    {
        if (GameManager.Instance.MainCanvas != null)
            _singleton.gameObject.transform.SetParent(GameManager.Instance.MainCanvas.transform);
        else
            _singleton.gameObject.transform.SetParent(FindObjectOfType<Canvas>().transform);

        Instance._Setup(message, buttons);

    }

    public IEnumerator Show_Coroutine(string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
    {
        Instance._Setup(message, buttons);

        yield return StartCoroutine(_Wait());

        gameObject.SetActive(false);
    }

    void _Setup(string message, MessageBoxButtons buttons)
    {
        result = DialogResult.None;
        text.text = message;
        _SetupButtons(buttons);
        transform.SetAsLastSibling();
        _singleton.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        gameObject.SetActive(true);
    }

    void _SetupButtons(MessageBoxButtons buttons)
    {
        switch(buttons)
        {
            case MessageBoxButtons.OK:
                buttonOK.gameObject.SetActive(true);
                buttonCancel.gameObject.SetActive(false);
                break;
            case MessageBoxButtons.OKCancel:
                buttonOK.gameObject.SetActive(true);
                buttonCancel.gameObject.SetActive(true);
                break;
        }
    }


    IEnumerator _Wait()
    {
        while(result == DialogResult.None)
        {
            yield return null;
        }
    }

    public void ButtonOk()
    {
        result = DialogResult.OK;
        Instance.gameObject.SetActive(false);
    }

    public void ButtonCancel()
    {
        result = DialogResult.Cancel;
        Instance.gameObject.SetActive(false);
    }



    private void OnDestroy()
    {
        if(_singleton == this)
        _singleton = null;
    }
}