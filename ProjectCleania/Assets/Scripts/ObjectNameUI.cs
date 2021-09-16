using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ��� : ���콺�� ������Ʈ ���� �ø���, ��ü ������ ���� '�̸� ǥ�� & ���콺 ��� ����'�� ����ȴ�.
// �ֿ� ���� : ��ü ����, �̸�, ���콺 ���, UI ����, UI ǥ�� ��ġ
public class ObjectNameUI : ObjectUI
{
    public string DisplayName;                      // �̸�
    public string StateName;                        // ���� �̸�
    public GameObject MouseObject;                  // ���콺 ���
    public bool WorldCoordinate = false;            // UI ǥ�� ����
    public Vector2 UIShowPosition = Vector2.zero;   // UI ǥ�� ��ġ

    private Text nameText;
    private Text stateText;

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
            if (!uiObjectInstExist)
                base.InstantiateUIObject();
        }
        else
        {
            DestroyUI();
        }
    }

    protected override void InstantiateUIObject()
    {
        base.InstantiateUIObject();

        foreach (Text infoText in textComponents)
        {
            if (infoText.name == "Name")
                infoText.text = DisplayName;
            if (infoText.name == "State")
                infoText.text = StateName;
        }
    }

    void UpdateUIPosition()
    {
        if (!uiObjectInstExist) return;

        Vector3 dy = Vector3.up * UIShowPosition.y;
        dy.y += (ObjectMainCollider.size.y * 0.5f); 
        Vector3 dx = Vector3.right * UIShowPosition.x;
        uiObjectInst.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + ObjectMainCollider.center + dy - dx);
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
}


#region

//// ��� : ���콺�� ������Ʈ ���� �ø���, ��ü ������ ���� '�̸� ǥ�� & ���콺 ��� ����'�� ����ȴ�.
//// �ֿ� ���� : ��ü ����, �̸�, ���콺 ���, UI ����, UI ǥ�� ��ġ
//public class ObjectNameUI : MonoBehaviour
//{
//    public string DisplayName;                      // �̸�
//    public string StateName;                        // ���� �̸�
//    public GameObject MouseObject;                  // ���콺 ���
//    public GameObject UIObject;                     // UI ����
//    public bool WorldCoordinate = false;            // UI ǥ�� ����
//    public Vector2 UIShowPosition = Vector2.zero;   // UI ǥ�� ��ġ

//    private Image[] hpBars;
//    private Text[] infoTexts;
//    private Text nameText;
//    private Text stateText;
//    private GameObject UIObjectInst = null;
//    private bool UIObjectInstExist
//    {
//        get
//        {
//            if (UIObjectInst == null)
//                return false;
//            else
//                return true;
//        }
//    }
//    private BoxCollider ObjectMainCollider;

//    private void Awake()
//    {
//        ObjectMainCollider = GetComponent<BoxCollider>();
//    }

//    private void Start()
//    {
//        ActiveUI(false);
//    }

//    private void Update()
//    {
//        JudgeCreateDestroy();
//    }

//    private void FixedUpdate()
//    {
//        if (IsMouseCollide())
//        {
//            ActiveUI(true);

//            if (WorldCoordinate)
//                UpdateUIPosition();
//        }
//        else
//            ActiveUI(false);
//    }

//    void JudgeCreateDestroy()
//    {
//        if (IsInUIBorder(Camera.main.WorldToScreenPoint(this.transform.position)))
//        {
//            if (!UIObjectInstExist)
//                InstantiateUIObject();
//        }
//        else
//        {
//            DestroyUI();
//        }
//    }

//    bool IsInUIBorder(Vector3 point)
//    {
//        return (Screen.width * 0.8f) > point.x &&
//               (Screen.width * (1 - 0.8f)) < point.x &&
//               (Screen.height * 0.8f) > point.y &&
//               (Screen.height * (1 - 0.8f)) < point.y;
//    }

//    void InstantiateUIObject()
//    {
//        UIObjectInst = Instantiate(UIObject, FindObjectOfType<Canvas>().transform);

//        hpBars = UIObjectInst.GetComponentsInChildren<Image>();
//        infoTexts = UIObjectInst.GetComponentsInChildren<Text>();

//        foreach (Text infoText in infoTexts)
//        {
//            if (infoText.name == "Name")
//                infoText.text = DisplayName;
//            if (infoText.name == "State")
//                infoText.text = StateName;
//        }
//    }

//    void ActiveUI(bool value)
//    {
//        if (!UIObjectInstExist) return;

//        foreach (Image bar in hpBars)
//            bar.enabled = value;

//        foreach (Text infoText in infoTexts)
//            infoText.enabled = value;
//    }

//    void UpdateUIPosition()
//    {
//        if (!UIObjectInstExist) return;

//        Vector3 dy = Vector3.up * UIShowPosition.y;
//        dy.y += (ObjectMainCollider.size.y * 0.5f);
//        Vector3 dx = Vector3.right * UIShowPosition.x;
//        UIObjectInst.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + ObjectMainCollider.center + dy - dx);
//    }

//    bool IsMouseCollide()
//    {
//        bool isCollide = false;

//        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
//        RaycastHit raycastHit;

//        if (Physics.Raycast(ray, out raycastHit))
//        {
//            if (raycastHit.transform.gameObject == this.transform.gameObject)
//                isCollide = true;
//        }

//        return isCollide;
//    }

//    public void DestroyUI()
//    {
//        if (UIObjectInstExist)
//            Destroy(UIObjectInst);
//        UIObjectInst = null;
//    }
//}

#endregion