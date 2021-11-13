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
    //MessageBoxButtons buttons;

    static UI_MessageBox _singleton;
    static public UI_MessageBox Instance
    {
        get
        {
            if (_singleton == null)
            {
                _singleton = ((GameObject)Instantiate(
                    Resources.Load("Prefabs/UI_MessageBox"),
                    GameManager.Instance.MainCanvas.transform))
                    .GetComponent<UI_MessageBox>();

                _singleton.gameObject.SetActive(false);
            }
            return _singleton;
        }
    }

    static public DialogResult Show(string message, MessageBoxButtons buttons = MessageBoxButtons.OK)
    {
        Instance._Setup(message, buttons);

        Instance.Show();

        Instance.gameObject.SetActive(false);
        return Instance.result;
    }


    void _Setup(string message, MessageBoxButtons buttons)
    {
        result = DialogResult.None;
        text.text = message;
        _SetupButtons(buttons);
        transform.SetAsLastSibling();
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

    IEnumerator Show()
    {
        yield return StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        while(result == DialogResult.None)
        {
            yield return null;
        }
    }

    public void ButtonOk()
    {
        result = DialogResult.OK;
    }

    public void ButtonCancel()
    {
        result = DialogResult.Cancel;
    }



    private void OnDestroy()
    {
        _singleton = null;
    }
}