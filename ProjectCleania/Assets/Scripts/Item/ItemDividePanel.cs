using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDividePanel : MonoBehaviour
{
    public UI_ItemController controller;

    public InputField inputField;
    public Slider slider;
    public float nDivide;





    private void Start()
    {
        inputField.text = slider.value.ToString();
    }

    public void SetMaxValue(float value)
    {
        slider.maxValue = value;
    }

    public void UpdateValue()
    {
        nDivide = slider.value;
        inputField.text = slider.value.ToString();
    }


    public void InputValue()
    {
        if (inputField.text == "")
            slider.value = 0;
        else
        {
            float temp = float.Parse(inputField.text);
            slider.value = temp < slider.maxValue ? temp : slider.maxValue;
        }
    }





    public void OnClickedUp()
    {
        slider.value += 1;
    }

    public void OnClickedDown()
    {
        slider.value -= 1;
    }

    public void OnClickedOK()
    {
        //
        gameObject.SetActive(false);
    }

    public void OnClickedCancel()
    {
        gameObject.SetActive(false);
    }

}
