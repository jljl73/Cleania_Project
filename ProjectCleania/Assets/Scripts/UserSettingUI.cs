using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UserSetting â ����
// 1. ���� ��ư Ŭ���� ���� ������ â�� �ٲ�� ���
// 2. ���� ���� ���� ���� �� �ҷ�����
public class UserSettingUI : MonoBehaviour
{
    // -------------------------------------------------------------------------//
    // 1. ���� ��ư Ŭ���� ���� ������ â�� �ٲ�� ���
    // -------------------------------------------------------------------------//
    public List<GameObject> LeftButtons;
    public List<GameObject> RightContents;

    private Dictionary<GameObject, GameObject> left2Right;

    // -------------------------------------------------------------------------//
    // 2. ���� ���� ���� ���� �� �ҷ�����
    // -------------------------------------------------------------------------//
    //private bool isFullScreenMode;
    //public bool IsFullScreenMode { get { return isFullScreenMode; }}
    private float videoBrightness;
    public float VideoBrightness { get { return videoBrightness; }}

    public Dropdown DropdownScreenMode;
    public Dropdown DropdownScreenResolution;
    public Scrollbar ScrollbarVideoBrightness;
    public Image VideoBrightnessFillImage;

    Resolution[] screenResolutions;

    private void Awake()
    {
        left2Right = new Dictionary<GameObject, GameObject>();
        
        for (int i = 0; i < LeftButtons.Count; i++)
        {
            left2Right.Add(LeftButtons[i], RightContents[i]);
        }
        LoadData();

    }

    private void Start()
    {
        // ���� ����Ʈ ����
        DropdownScreenResolution.ClearOptions();
        // ������ �ػ� �޾ƿ���
        screenResolutions = Screen.resolutions;
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;
        for (int i = 0; i < screenResolutions.Length; i++)
        {
            options.Add(screenResolutions[i].width.ToString() + " x " + screenResolutions[i].height.ToString());
            if (screenResolutions[i].width.ToString() == Screen.currentResolution.width.ToString() &&
                screenResolutions[i].height.ToString() == Screen.currentResolution.height.ToString())
            {
                currentResolutionIndex = i;
            }
        }
        // ����Ʈ�� �߰�
        DropdownScreenResolution.AddOptions(options);
        DropdownScreenResolution.value = currentResolutionIndex;
        DropdownScreenResolution.RefreshShownValue();
    }

    private void OnEnable()
    {
        // ù��° ĭ�� �⺻ �������� ����
        LeftButtons[0].GetComponent<Button>().Select();
        ChangeRightContentReferTo(LeftButtons[0]);
    }

    void Update()
    {

    }

    public void LoadData()
    {
        // ��ũ�� ��� ���ε�
        if(PlayerPrefs.GetInt("ScreenMode") == 0)
        {
            DropdownScreenMode.value = 0;
            //isFullScreenMode = true;
            Screen.fullScreen = true;
        }
        else
        {
            DropdownScreenMode.value = 1;
            //isFullScreenMode = false;
            Screen.fullScreen = false;
        }
        DropdownScreenMode.RefreshShownValue();

        // ���� ��� ���ε�
        //     ������ ������Ʈ
        videoBrightness = PlayerPrefs.GetFloat("VideoBrightness");
        //     �̹��� ������Ʈ
        VideoBrightnessFillImage.fillAmount = videoBrightness;
        ScrollbarVideoBrightness.value = videoBrightness;
    }

    public void ChangeRightContentReferTo(GameObject buttonObj)
    {
        for (int i = 0; i < RightContents.Count; i++)
        {
            RightContents[i].SetActive(false);
        }
        left2Right[buttonObj].SetActive(true);
    }

    public void ChangeScreenMode(int value)
    {
        PlayerPrefs.SetInt("ScreenMode", value);

        if (DropdownScreenMode.value == 0)
        {
            //isFullScreenMode = true;
            Screen.fullScreen = true;
        }
        else
        {
            //isFullScreenMode = false;
            Screen.fullScreen = false;
        }
    }

    public void ChangeScreenResolution(int index)
    {
        Resolution resolution = screenResolutions[index];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void ChangeVideoBrightness(float value)
    {
        videoBrightness = value;

        PlayerPrefs.SetFloat("VideoBrightness", videoBrightness);

        VideoBrightnessFillImage.fillAmount = videoBrightness;

    }
}
