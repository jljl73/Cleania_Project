using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MouseOverDetailUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler 
{
    public enum DetailUIType { Hp, Mp, GameUIIcon, Skill };

    public GameObject DetailGameObject;
    private GameObject PlayerGameObject;

    private AbilityStatus playerStatus;
    private ContentSizeFitter[] DetailGameObjectChildrenCSF;
    private GameObject uiDetailObj;
    private GameObject runeDetailObj;
    private GameObject uiTitleObj;

    public bool IsRuneExist;
    public DetailUIType UIType = DetailUIType.Hp;

    public string Title;
    public string RuneName;
    [TextArea]
    public string State;
    [TextArea]
    public string Detail;
    public Sprite RuneImage;
    [TextArea]
    public string RunDetail;

    private Text title;
    private Text runeName;
    private Text state;
    private Text detail;
    private Image runeImage;
    private Text runeDetail;

    private Text[] textComponents;
    private Image[] imageComponents;

    void Awake()
    {
        //playerStatus = FindObjectOfType<PlayerMovement>().gameObject.GetComponent<AbilityStatus>();

        textComponents = DetailGameObject.GetComponentsInChildren<Text>();
        imageComponents = DetailGameObject.GetComponentsInChildren<Image>();

        for (int i = 0; i < textComponents.Length; i++)
        {
            if (textComponents[i].name == "Title")
            {
                title = textComponents[i];
                title.text = Title;
            }
            else if (textComponents[i].name == "RuneName")
            {
                runeName = textComponents[i];
                runeName.text = RuneName;
            }
            else if (textComponents[i].name == "State")
            {
                state = textComponents[i];
                state.text = State;
            }

            else if (textComponents[i].name == "Detail")
            {
                detail = textComponents[i];
                detail.text = Detail;
            }
            else if (textComponents[i].name == "RuneDetail")
            {
                runeDetail = textComponents[i];
                runeDetail.text = RunDetail;
            }
        }

        for (int i = 0; i < imageComponents.Length; i++)
        {
            if (imageComponents[i].name == "RuneImage")
            {
                runeImage = imageComponents[i];
                runeImage.sprite = RuneImage;
            }
        }

        DetailGameObjectChildrenCSF = DetailGameObject.GetComponentsInChildren<ContentSizeFitter>();
        for (int i = 0; i < DetailGameObjectChildrenCSF.Length; i++)
        {
            if (DetailGameObjectChildrenCSF[i].gameObject.name == "UITitles")
            {
                uiTitleObj = DetailGameObjectChildrenCSF[i].gameObject;
            }
            else if (DetailGameObjectChildrenCSF[i].gameObject.name == "UIDetails")
            {
                uiDetailObj = DetailGameObjectChildrenCSF[i].gameObject;
            }
            else if (DetailGameObjectChildrenCSF[i].gameObject.name == "RuneDetail")
            {
                runeDetailObj = DetailGameObjectChildrenCSF[i].gameObject;
            }
        }
    }

    void Start()
    {
        switch (UIType)
        {
            case DetailUIType.Hp:
            case DetailUIType.Mp:
                uiTitleObj.SetActive(false);
                uiDetailObj.SetActive(true);
                state.gameObject.SetActive(true);
                detail.gameObject.SetActive(true);
                runeDetailObj.SetActive(false);
                break;
            case DetailUIType.GameUIIcon:
                title.gameObject.SetActive(true);
                runeName.gameObject.SetActive(false);
                uiDetailObj.SetActive(false);
                runeDetailObj.SetActive(false);
                break;
            case DetailUIType.Skill:
                uiTitleObj.SetActive(true);
                uiDetailObj.SetActive(true);
                title.gameObject.SetActive(true);
                state.gameObject.SetActive(true);
                detail.gameObject.SetActive(true);
                if (IsRuneExist)
                {
                    runeDetailObj.SetActive(true);
                    runeName.gameObject.SetActive(true);
                    runeImage.gameObject.SetActive(true);
                    runeDetail.gameObject.SetActive(true);
                }
                else
                {
                    runeDetailObj.SetActive(false);
                }
                break;
            default:
                break;
        }
    }

    void Update()
    {
        UpdateText();
    }

    void UpdateText()
    {
        switch (UIType)
        {
            case DetailUIType.Hp:
                state.text = "생명력: " + playerStatus.HP.ToString() + "/" + playerStatus[Ability.Stat.MaxHP].ToString();
                break;
            case DetailUIType.Mp:
                state.text = "청량감: " + playerStatus.MP.ToString() + "/" + playerStatus[Ability.Stat.MaxMP].ToString();
                break;
            case DetailUIType.GameUIIcon:
                break;
            case DetailUIType.Skill:
                //title.gameObject.SetActive(true);
                //state.gameObject.SetActive(true);
                state.text = "생성 고유자원: " + "" + "\r\n" + "소모 고유자원: " + "" + "\r\n" + "재사용 대기시간:" + "";
                //detail.gameObject.SetActive(true);
                //if (IsRuneExist)
                //{
                //    runeName.gameObject.SetActive(true);
                //    runeImage.gameObject.SetActive(true);
                //    runeDetail.gameObject.SetActive(true);
                //}
                //else
                //{
                //    runeName.gameObject.SetActive(false);
                //    runeImage.gameObject.SetActive(false);
                //    runeDetail.gameObject.SetActive(false);
                //}
                break;
            default:
                break;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        print("OnPointerEnter");
        DetailGameObject.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        print("OnPointerExit");
        DetailGameObject.SetActive(false);
    }
}
