using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// UserSetting 창 괸리
// 1. 왼쪽 버튼 클릭에 따라 오른쪽 창이 바뀌는 기능
// 2. 유저 셋팅 상태 저장 및 불러오기
public class UserSettingUI : MonoBehaviour
{
    // -------------------------------------------------------------------------//
    // 1. 왼쪽 버튼 클릭에 따라 오른쪽 창이 바뀌는 기능
    // -------------------------------------------------------------------------//
    public List<GameObject> LeftButtons;
    public List<GameObject> RightContents;

    private Dictionary<GameObject, GameObject> left2Right;

    // -------------------------------------------------------------------------//
    // 2. 유저 셋팅 상태 저장 및 불러오기
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
        // 기존 리스트 삭제
        DropdownScreenResolution.ClearOptions();
        // 가능한 해상도 받아오기
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
        // 리스트에 추가
        DropdownScreenResolution.AddOptions(options);
        DropdownScreenResolution.value = currentResolutionIndex;
        DropdownScreenResolution.RefreshShownValue();
    }

    private void OnEnable()
    {
        // 첫번째 칸을 기본 세팅으로 설정
        LeftButtons[0].GetComponent<Button>().Select();
        ChangeRightContentReferTo(LeftButtons[0]);
    }

    void Update()
    {

    }

    public void LoadData()
    {
        // 스크린 모드 업로드
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

        // 비디오 밝기 업로드
        //     데이터 업데이트
        videoBrightness = PlayerPrefs.GetFloat("VideoBrightness");
        //     이미지 업데이트
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
