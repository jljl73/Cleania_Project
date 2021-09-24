using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AutoNameUI : ObjectUI
{
    public enum ItemRank { Normal, Rare, Legend, Quest };
    public ItemRank ItemRankType = ItemRank.Normal;

    public string ObjectName = "";

    public float UIyPos = 1f;
    public float UIxPos = 0f;

    public bool AlwaysShowByKeyToggle = true;
    public bool ShowOnlyByKeyPress = false;
    public bool ShowFor10Sec = false;
    private bool IsAlwaysShow = true;
    private bool IsShowFor10SecIng = false;

    void Update()
    {
        base.JudgeCreateDestroy();

        SetItemNameShowSetting();

        SetActiveByRule();

        UpdateUIPosition();
    }

    protected override void InstantiateUIObject()
    {
        base.InstantiateUIObject();

        SetTextColor();

        // Set Name
        if (GetTextComponent("Name") != null)
            GetTextComponent("Name").text = ObjectName;
    }

    void SetActiveByRule()
    {
        if (AlwaysShowByKeyToggle)
        {
            if (IsAlwaysShow)
            {
                if (Input.GetKeyDown(KeyCode.LeftAlt))
                    IsAlwaysShow = false;
                else
                    ActiveUI(true);
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.LeftAlt))
                    IsAlwaysShow = true;
                else
                    ActiveUI(false);
            }
            return;
        }

        if (ShowOnlyByKeyPress)
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                ActiveUI(true);
                return;
            }
            else
            {
                ActiveUI(false);
                return;
            }
        }

        if (ShowFor10Sec)
        {
            if (Input.GetKey(KeyCode.LeftAlt))
            {
                if (!IsShowFor10SecIng)
                    StartCoroutine("ShowItemFor", 10f);
            }
            return;
        }
    }

    IEnumerator ShowItemFor(float t)
    {
        IsShowFor10SecIng = true;
        ActiveUI(true);
        yield return new WaitForSeconds(t);
        ActiveUI(false);
        IsShowFor10SecIng = false;
    }

    void SetItemNameShowSetting()
    {
        if (AlwaysShowByKeyToggle)
        {
            ShowOnlyByKeyPress = false;
            ShowFor10Sec = false;
        }
        else if (ShowOnlyByKeyPress)
        {
            AlwaysShowByKeyToggle = false;
            ShowFor10Sec = false;
        }
        else if (ShowFor10Sec)
        {
            AlwaysShowByKeyToggle = false;
            ShowOnlyByKeyPress = false;
        }
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

        // Set color
        if (GetTextComponent("Name") != null)
            GetTextComponent("Name").color = color;
    }

    void UpdateUIPosition()
    {
        if (!uiObjectInstExist) return;

        Vector3 dy = Vector3.up * UIyPos;
        Vector3 dx = Vector3.right * UIxPos;
        uiObjectInst.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + dy - dx);
    }

    private void OnDestroy()
    {
        base.DestroyUI(); ;
    }
}


// 상속 전 코드
#region


//public class ItemNameUI : MonoBehaviour
//{
//    public enum ItemRank { Normal, Rare, Legend, Quest };

//    public ItemRank ItemRankType = ItemRank.Normal;

//    public GameObject NameUIObject;
//    private GameObject nameUIObjInst;
//    public string ObjectName = "";
//    private bool nameIObjectInstExist
//    {
//        get
//        {
//            if (nameUIObjInst == null)
//                return false;
//            else
//                return true;
//        }
//    }

//    private Image backgroundImg;
//    private Text nameText;

//    public float UIyPos = 1f;
//    public float UIxPos = 0f;

//    public bool AlwaysShowByKeyToggle = true;
//    public bool ShowOnlyByKeyPress = false;
//    public bool ShowFor10Sec = false;
//    private bool IsAlwaysShow = true;
//    private bool IsShowFor10SecIng = false;

//    public float UIShowBorderPercent = 0.8f;

//    void Update()
//    {
//        SetItemNameShowSetting();

//        SetActiveByRule();

//        UpdateUIPosition();
//    }

//    void InstantiateUIObject()
//    {
//        // 이름 UI만든 후 셋팅 설정
//        nameUIObjInst = Instantiate(NameUIObject, FindObjectOfType<Canvas>().transform);
//        //nameUIObjInst.GetComponent<ObjectOwnerInfo>().OwnerObject = NameUIObject;

//        // 조작 필요 컴포넌트 설정
//        backgroundImg = nameUIObjInst.GetComponentInChildren<Image>();
//        nameText = nameUIObjInst.GetComponentInChildren<Text>();

//        SetTextColor();

//        nameText.text = ObjectName;
//    }

//    void SetActiveByRule()
//    {
//        if (IsInUIBorder(Camera.main.WorldToScreenPoint(this.transform.position)))
//        {
//            if (!nameIObjectInstExist)
//                InstantiateUIObject();
//        }
//        else
//        {
//            ActiveUI(false);
//            DestroyUI();
//            return;
//        }

//        if (AlwaysShowByKeyToggle)
//        {
//            if (IsAlwaysShow)
//            {
//                if (Input.GetKeyDown(KeyCode.LeftAlt))
//                    IsAlwaysShow = false;
//                else
//                    ActiveUI(true);
//            }
//            else
//            {
//                if (Input.GetKeyDown(KeyCode.LeftAlt))
//                    IsAlwaysShow = true;
//                else
//                    ActiveUI(false);
//            }
//            return;
//        }

//        if (ShowOnlyByKeyPress)
//        {
//            if (Input.GetKey(KeyCode.LeftAlt))
//            {
//                ActiveUI(true);
//                return;
//            }
//            else
//            {
//                ActiveUI(false);
//                return;
//            }
//        }

//        if (ShowFor10Sec)
//        {
//            if (Input.GetKey(KeyCode.LeftAlt))
//            {
//                if (!IsShowFor10SecIng)
//                    StartCoroutine("ShowItemFor", 10f);
//            }
//            return;
//        }
//    }

//    IEnumerator ShowItemFor(float t)
//    {
//        IsShowFor10SecIng = true;
//        ActiveUI(true);
//        yield return new WaitForSeconds(t);
//        ActiveUI(false);
//        IsShowFor10SecIng = false;
//    }

//    void SetItemNameShowSetting()
//    {
//        if (AlwaysShowByKeyToggle)
//        {
//            ShowOnlyByKeyPress = false;
//            ShowFor10Sec = false;
//        }
//        else if (ShowOnlyByKeyPress)
//        {
//            AlwaysShowByKeyToggle = false;
//            ShowFor10Sec = false;
//        }
//        else if (ShowFor10Sec)
//        {
//            AlwaysShowByKeyToggle = false;
//            ShowOnlyByKeyPress = false;
//        }
//    }

//    bool IsInUIBorder(Vector3 point)
//    {
//        return (Screen.width * UIShowBorderPercent) > point.x &&
//               (Screen.width * (1 - UIShowBorderPercent)) < point.x &&
//               (Screen.height * UIShowBorderPercent) > point.y &&
//               (Screen.height * (1 - UIShowBorderPercent)) < point.y;
//    }

//    void SetTextColor()
//    {
//        Color color = Color.white;
//        switch (ItemRankType)
//        {
//            case ItemRank.Rare:
//                color = new Color32(65, 105, 255, 255); // 로얄 블루
//                break;
//            case ItemRank.Legend:
//                color = new Color32(255, 217, 0, 255); // 골드
//                break;
//            case ItemRank.Quest:
//                color = new Color32(153, 50, 204, 255); // 다크 오치드
//                break;
//        }

//        nameText.color = color;
//    }

//    void ActiveUI(bool value)
//    {
//        if (!nameIObjectInstExist) return;

//        backgroundImg.enabled = value;
//        nameText.enabled = value;
//    }

//    void UpdateUIPosition()
//    {
//        if (!nameIObjectInstExist) return;

//        Vector3 dy = Vector3.up * UIyPos;
//        Vector3 dx = Vector3.right * UIxPos;
//        nameUIObjInst.transform.position = Camera.main.WorldToScreenPoint(this.transform.position + dy - dx);
//    }

//    public void DestroyUI()
//    {
//        if (nameIObjectInstExist)
//            Destroy(nameUIObjInst);
//        nameUIObjInst = null;
//    }
//}

#endregion