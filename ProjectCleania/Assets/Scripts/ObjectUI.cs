using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectUI : MonoBehaviour
{
    public float UIShowBorderPercent = 0.8f;
    public GameObject UIObject;
    protected GameObject uiObjectInst;
    protected bool uiObjectInstExist
    {
        get
        {
            if (uiObjectInst == null)
                return false;
            else
                return true;
        }
    }

    protected Image[] imageComponents;
    protected Text[] textComponents;

    protected virtual void InstantiateUIObject()
    {
        uiObjectInst = Instantiate(UIObject, FindObjectOfType<Canvas>().transform);

        imageComponents = uiObjectInst.GetComponentsInChildren<Image>();
        textComponents = uiObjectInst.GetComponentsInChildren<Text>();
    }

    protected virtual bool IsInUIBorder(Vector3 point)
    {
        return (Screen.width * UIShowBorderPercent) > point.x &&
               (Screen.width * (1 - UIShowBorderPercent)) < point.x &&
               (Screen.height * UIShowBorderPercent) > point.y &&
               (Screen.height * (1 - UIShowBorderPercent)) < point.y;
    }

    protected virtual void ActiveUI(bool value)
    {
        if (!uiObjectInstExist) return;

        foreach (Image image in imageComponents)
            image.enabled = value;

        foreach (Text text in textComponents)
            text.enabled = value;
    }

    public virtual void DestroyUI()
    {
        if (uiObjectInstExist)
            Destroy(uiObjectInst);
        uiObjectInst = null;
    }
}
