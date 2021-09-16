using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 기능 : 마우스를 오브젝트 위에 올리면, 객체 종류에 따라 '이름 표시 & 마우스 모양 변경'이 실행된다.
// 주요 변수 : 객체 종류, 이름, 마우스 모양, UI 종류, UI 표시 위치
public class ObjectNameUI : MonoBehaviour
{
    public string DisplayName;                      // 이름
    public string StateName;                        // 상태 이름
    public GameObject MouseObject;                  // 마우스 모양
    public GameObject UIObject;                     // UI 종류
    public bool WorldCoordinate = false;            // UI 표시 공간
    public Vector2 UIShowPosition = Vector2.zero;   // UI 표시 위치

    private Image[] hpBars;
    private Text[] infoTexts;
    private Text nameText;
    private Text stateText;
    private GameObject UIObjectInst = null;
    private bool UIObjectInstExist
    {
        get
        {
            if (UIObjectInst == null)
                return false;
            else
                return true;
        }
    }
    private BoxCollider ObjectMainCollider;

    private void Awake()
    {
        ObjectMainCollider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        ActiveUI(false);
    }

    private void Update()
    {
        JudgeCreateDestroy();
    }

    private void FixedUpdate()
    {
        if (IsMouseCollide())
        {
            ActiveUI(true);

            if (WorldCoordinate)
                UpdateUIPosition();
        }
        else
            ActiveUI(false);
    }

    void JudgeCreateDestroy()
    {
        if (IsInUIBorder(Camera.main.WorldToScreenPoint(this.transform.position)))
        {
            if (!UIObjectInstExist)
                InstantiateUIObject();
        }
        else
        {
            DestroyUI();
        }
    }

    bool IsInUIBorder(Vector3 point)
    {
        return (Screen.width * 0.8f) > point.x &&
               (Screen.width * (1 - 0.8f)) < point.x &&
               (Screen.height * 0.8f) > point.y &&
               (Screen.height * (1 - 0.8f)) < point.y;
    }

    void InstantiateUIObject()
    {
        UIObjectInst = Instantiate(UIObject, FindObjectOfType<Canvas>().transform);

        hpBars = UIObjectInst.GetComponentsInChildren<Image>();
        infoTexts = UIObjectInst.GetComponentsInChildren<Text>();

        foreach (Text infoText in infoTexts)
        {
            if (infoText.name == "Name")
                infoText.text = DisplayName;
            if (infoText.name == "State")
                infoText.text = StateName;
        }
    }

    void ActiveUI(bool value)
    {
        if (!UIObjectInstExist) return;

        foreach (Image bar in hpBars)
            bar.enabled = value;

        foreach (Text infoText in infoTexts)
            infoText.enabled = value;
    }

    void UpdateUIPosition()
    {
        if (!UIObjectInstExist) return;

        Vector3 dy = Vector3.up * UIShowPosition.y;
        dy.y += (ObjectMainCollider.size.y * 0.5f); 
        Vector3 dx = Vector3.right * UIShowPosition.x;
        UIObjectInst.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + ObjectMainCollider.center + dy - dx);
    }

    bool IsMouseCollide()
    {
        bool isCollide = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.transform.gameObject == this.transform.gameObject)
                isCollide = true;
        }

        return isCollide;
    }

    public void DestroyUI()
    {
        if (UIObjectInstExist)
            Destroy(UIObjectInst);
        UIObjectInst = null;
    }
}
