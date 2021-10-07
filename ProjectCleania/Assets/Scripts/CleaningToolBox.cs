using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;
using UnityEngine.UI;

public class CleaningToolBox : InteractionObject
{
    public float InteractionTime = 0f;
    float interactedTime = 0f;

    void Awake()
    {
        //onTriggerNameUI = GetComponent<OnTriggerNameUI>();
        //if (onTriggerNameUI.uiObjectInstExist)
        //{
        //    Image[] images = onTriggerNameUI.uiObjectInst.GetComponentsInChildren<Image>();
        //    foreach (Image image in images)
        //    {
        //        if (image.name == "Fill")
        //        {
        //            fillImage = image;
        //            fillImage.gameObject.SetActive(false);
        //        }
        //        if (image.name == "Background")
        //        {
        //            background = image;
        //            background.gameObject.SetActive(false);
        //        }
        //    }
        //}
    }

    void Update()
    {
        if ((int)InteractionTime != 0)
        {
            //if (onTriggerNameUI.uiObjectInstExist)
            //    fillImage.fillAmount = interactedTime / InteractionTime;
        }
    }

    public override void UsedTo(Collider target)
    {

    }

    //new void OnTriggerStay(Collider other)
    //{
    //    if (isUsed)
    //    {
    //        if (onTriggerNameUI.uiObjectInstExist)
    //        {
    //            background.gameObject.SetActive(false);
    //            fillImage.gameObject.SetActive(false);
    //        }
    //        return;
    //    }

    //    if (other.CompareTag("Player") && Input.GetKey(KeyCode.G))
    //    {
    //        interactedTime += Time.deltaTime;
    //        if (interactedTime > InteractionTime)
    //        {
    //            if (onTriggerNameUI.uiObjectInstExist)
    //            {
    //                background.gameObject.SetActive(true);
    //                fillImage.gameObject.SetActive(true);
    //            }

    //            // PopUpDetail
    //            this.UsedTo(other);
    //            isUsed = true;
    //        }
    //        return;
    //    }
    //    interactedTime = 0f;
    //}
}
