using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserSettingUI : MonoBehaviour
{
    public List<GameObject> LeftButtons;
    public List<GameObject> RightContents;

    private Dictionary<GameObject, GameObject> left2Right;

    private void Awake()
    {
        left2Right = new Dictionary<GameObject, GameObject>();

        for (int i = 0; i < LeftButtons.Count; i++)
        {
            left2Right.Add(LeftButtons[i], RightContents[i]);
        }
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

    public void ChangeRightContentReferTo(GameObject buttonObj)
    {
        for (int i = 0; i < RightContents.Count; i++)
        {
            RightContents[i].SetActive(false);
        }
        left2Right[buttonObj].SetActive(true);
    }
}
