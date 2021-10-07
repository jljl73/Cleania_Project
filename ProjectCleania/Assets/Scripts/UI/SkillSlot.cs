using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SkillSlot : MonoBehaviour, IPointerDownHandler, IPointerClickHandler, IPointerUpHandler
{
    public Canvas canvas;
    public string skillName = "";
    public Text text;
    bool isTracking;
    Vector3 screenPoint;

    void OnOffTracking(bool value)
    {
        isTracking = value;
    }

    void TrackMouse()
    {
        RectTransformUtility.ScreenPointToWorldPointInRectangle(
            transform.GetComponent<RectTransform>(),
            Input.mousePosition, canvas.worldCamera, out screenPoint);

        transform.position = screenPoint;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        text.text = skillName;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        GameObject ClickedObj = Instantiate(gameObject);
        ClickedObj.GetComponent<RectTransform>().position = GetComponent<RectTransform>().position;
        OnOffTracking(true);
        ClickedObj.transform.SetParent(transform.parent);
    }

    // Update is called once per frame
    void Update()
    {
        if (isTracking) TrackMouse();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isTracking) Destroy(gameObject);
        //OnOffTracking(false);
    }
}
