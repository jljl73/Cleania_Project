using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TestCleaningToolBox : OnTriggerNameUI
{
    protected bool isUsed = false;
    public bool IsUsed { get { return isUsed; } }
    public float InteractionTime = 0f;
    float interactedTime = 0f;

    Image fillImage;
    Image background;

    public void UsedTo(Collider target)
    {

    }


    public void Reset()
    {
        isUsed = false;
    }

    new void Awake()
    {
        base.Awake();
    }

    new void Update()
    {
        base.Update();

        if ((int)InteractionTime != 0)
        {
            if (uiObjectInstExist)
            {
                fillImage = GetImageComponent("Fill");
                background = GetImageComponent("Background");

                fillImage.fillAmount = interactedTime / InteractionTime;
            }
        }
    }

    new void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (uiObjectInstExist)
        {
            background.gameObject.SetActive(false);
            fillImage.gameObject.SetActive(false);
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (isUsed)
        {
            if (uiObjectInstExist)
            {
                background.gameObject.SetActive(false);
                fillImage.gameObject.SetActive(false);
            }
            return;
        }

        if (other.CompareTag("Player") && Input.GetKey(KeyCode.Space))
        {
            interactedTime += Time.deltaTime;
            print("interactedTime: " + interactedTime);
            if (uiObjectInstExist)
            {
                background.gameObject.SetActive(true);
                fillImage.gameObject.SetActive(true);
            }

            if (interactedTime > InteractionTime)
            {
                // PopUpDetail
                this.UsedTo(other);
                isUsed = true;
            }
            return;
        }
        else if (other.CompareTag("Player"))
        {
            interactedTime = 0f;
        }
    }
}
