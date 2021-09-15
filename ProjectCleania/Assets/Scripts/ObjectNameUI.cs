using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectNameUI : MonoBehaviour
{
    public enum ItemRank { Normal, Rare, Legend, Quest };

    public ItemRank ItemRankType = ItemRank.Normal;

    public GameObject NameUIObject;
    private GameObject nameUIObjInst;
    public string ObjectName;

    private Image backgroundImg;
    private Text nameText;

    public float UIyPos = 1f;
    public float UIxPos = 0f;

    public bool ShowByMouseOn = false;
    public float UIShowBorderPercent = 0.8f;

    private void Awake()
    {
        nameUIObjInst = Instantiate(NameUIObject, FindObjectOfType<Canvas>().transform);

        backgroundImg = nameUIObjInst.GetComponentInChildren<Image>();
        nameText = nameUIObjInst.GetComponentInChildren<Text>();
    }

    private void Start()
    {
        backgroundImg.enabled = false;
        nameText.enabled = false;

        SetTextColor();

        if (!ShowByMouseOn)
            ActiveUI(true);
    }

    private void OnEnable()
    {
        nameText.text = ObjectName;
    }

    void Update()
    {
        SetByUserSetting();

        UpdateUIPosition();

        SetActiveByRule();
    }

    void SetByUserSetting()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            ShowByMouseOn = ShowByMouseOn == true ? false : true;
        }
    }

    void SetActiveByRule()
    {
        if (!IsInUIBorder(Camera.main.WorldToScreenPoint(this.transform.position)))
        {
            ActiveUI(false);
            return;
        }

        if (!ShowByMouseOn)
        {
            ActiveUI(true);
            return;
        }

        if (IsMouseCollide())
            ActiveUI(true);
        else
            ActiveUI(false);
    }

    bool IsInUIBorder(Vector3 point)
    {
        return (Screen.width * UIShowBorderPercent) > point.x &&
               (Screen.width * (1 - UIShowBorderPercent)) < point.x &&
               (Screen.height * UIShowBorderPercent) > point.y &&
               (Screen.height * (1 - UIShowBorderPercent)) < point.y;
    }

    void SetTextColor()
    {
        Color color = Color.white;
        switch (ItemRankType)
        {
            case ItemRank.Rare:
                color = new Color32(65, 105, 255, 255); // 로얄 블루
                break;
            case ItemRank.Legend:
                color = new Color32(255, 217, 0, 255); // 골드
                break;
            case ItemRank.Quest:
                color = new Color32(153, 50, 204, 255); // 다크 오치드
                break;
        }

        nameText.color = color;
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
