using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectUI : MonoBehaviour
{
    public float BorderPercent = 0.8f;
    public GameObject UIObject;
    //public Canvas CanvasToDraw;

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

    private Image[] imageComponents;
    protected Image GetImageComponent(string name)
    {
        foreach (Image image in imageComponents)
        {
            if (image.name == name)
                return image;
        }
        return null;
    }
    private Text[] textComponents;
    protected Text GetTextComponent(string name)
    {
        foreach (Text text in textComponents)
        {
            if (text.name == name)
                return text;
        }
        return null;
    }

    protected virtual void InstantiateUIObject()
    {
        uiObjectInst = Instantiate(UIObject, GameManager.Instance.MainCanvas.transform);    // 메인 캔버스에 CanvasManager 넣어주세요

        imageComponents = uiObjectInst.GetComponentsInChildren<Image>();
        textComponents = uiObjectInst.GetComponentsInChildren<Text>();
        ActiveUI(false);
    }

    protected virtual bool IsInUIBorder(Vector3 point)
    {
        return (Screen.width * BorderPercent) > point.x &&
               (Screen.width * (1 - BorderPercent)) < point.x &&
               (Screen.height * BorderPercent) > point.y &&
               (Screen.height * (1 - BorderPercent)) < point.y;
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

    protected virtual void JudgeCreateDestroy()
    {
        if (IsInUIBorder(Camera.main.WorldToScreenPoint(this.transform.position)))
        {
            if (!uiObjectInstExist)
                InstantiateUIObject();
        }
        else
        {
            DestroyUI();
        }
    }
}
