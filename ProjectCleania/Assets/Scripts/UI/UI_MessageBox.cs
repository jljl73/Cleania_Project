using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_MessageBox : MonoBehaviour
{
    [SerializeField]
    Text text;

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

    static public void Close()
    {
        Instance.gameObject.SetActive(false);
    }

    static public void Message(string message)
    {
        Instance.text.text = message;
        Instance.transform.SetAsLastSibling();
        Instance.gameObject.SetActive(true);
    }

    public void Ok()
    {
        gameObject.SetActive(false);
    }


}
