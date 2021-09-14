using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectNameUI : MonoBehaviour
{
    public GameObject NameUIObject;
    private GameObject nameUIObjInst;
    public string ObjectName;

    private Image backgroundImg;
    private Text nameText;

    public float UIyPos = 1f;
    public float UIxPos = 0f;

    private void Awake()
    {
        nameUIObjInst = Instantiate(NameUIObject, GetComponentInChildren<Canvas>().transform);

        backgroundImg = nameUIObjInst.GetComponentInChildren<Image>();
        nameText = nameUIObjInst.GetComponentInChildren<Text>();
    }

    private void Start()
    {
        backgroundImg.enabled = false;
        nameText.enabled = false;
    }

    private void OnEnable()
    {
        nameText.text = ObjectName;
    }

    void Update()
    {
        UpdateUIPosition();

        if (IsMouseCollide())
        {
            ActiveUI(true);
        }
        else
        {
            ActiveUI(false);
        }
    }

    void ActiveUI(bool value)
    {
        backgroundImg.enabled = value;
        nameText.enabled = value;
    }

    void UpdateUIPosition()
    {
        Vector3 dy = Vector3.up * UIyPos;
        Vector3 dx = Vector3.right * UIxPos;
        nameUIObjInst.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + dy - dx);
    }

    bool IsMouseCollide()
    {
        bool isCollide = false;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit raycastHit;

        if (Physics.Raycast(ray, out raycastHit))
        {
            if (raycastHit.transform.gameObject == this.gameObject)
                isCollide = true;
        }

        return isCollide;
    }
}
