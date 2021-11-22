using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDividePanel : MonoBehaviour
{
    public UI_ItemController controller;

    [SerializeField]
    InputField inputField;
    [SerializeField]
    Slider slider;



    void Divide()
    {
        controller.currentContainer.AddSeparated(ItemInstance.Instantiate(controller.itemInstance.SO.ID, (int)slider.value));
        controller.itemInstance.Count -= (int)slider.value;

        int index = controller.currentContainer[controller];
        ItemInstance item = controller.itemInstance;
        UI_ItemContainer container = controller.currentContainer;
        container.Remove(index);
        container.Add(item, index);
    }


    private void OnEnable()
    {
        if (controller == null)
        {
            gameObject.SetActive(false);
            return;
        }
        slider.minValue = 1;
        slider.maxValue = controller.itemInstance.Count - 1;
        slider.value = 1;
        inputField.text = "1";
    }




    public void OnSliderValueChanged()
    {
        inputField.text = slider.value.ToString();
    }

    public void OnInputFieldValueChanged()
    {
        if (inputField.text == "")
            slider.value = slider.minValue;
        else
        {
            float temp = float.Parse(inputField.text);

            if (temp > slider.maxValue || temp < slider.minValue)
                OnSliderValueChanged();
            else
                slider.value = (int)temp;
        }
    }

    public void OnClickedUp()
    {
        if (slider.value < slider.maxValue)
            slider.value += 1;
    }

    public void OnClickedDown()
    {
        if (slider.value > slider.minValue)
            slider.value -= 1;
    }

    public void OnClickedOK()
    {
        Divide();
        gameObject.SetActive(false);
    }

    public void OnClickedCancel()
    {
        gameObject.SetActive(false);
    }

}
