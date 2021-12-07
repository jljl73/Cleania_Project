using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Text;

public class DepositPanel : MonoBehaviour
{
    [SerializeField]
    InputField Amount;
    [SerializeField]
    Text afterText;
    [SerializeField]
    UI_Currency storage;
    [SerializeField]
    UI_Currency otherStorage;

    StringBuilder sb = new StringBuilder();
    public string Title;

    void OnEnable()
    {
        OnValueChange();
    }


    public void OnValueChange()
    {
        sb.Clear();
        sb.Append(Title);
        sb.Append(" ÈÄ ±Ý¾× : ");

        int amount = 0;
        if (Amount.text != "")
            amount = int.Parse(Amount.text);


        if (storage.Crystal < amount)
            sb.Append("<Color=#FF7F50>");
        sb.Append((storage.Crystal - amount));

        if (storage.Crystal < amount)
            sb.Append("</Color>");

        afterText.text = sb.ToString();
    }

    public void Despoit()
    {
        sb.Clear();

        if (Amount.text == "") return;

        int amount = int.Parse(Amount.text);

        if (storage.Crystal < amount) return;

        storage.AddCrystal(-amount, UI_Currency.SourceType.Deposit);
        otherStorage.AddCrystal(amount, UI_Currency.SourceType.Deposit);
        Amount.text = "";
        gameObject.SetActive(false);
    }

}
