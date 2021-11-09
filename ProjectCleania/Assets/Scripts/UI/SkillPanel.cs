using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillPanel : MonoBehaviour
{
    public GameObject[] pages;
    public Text title;
    public Text skillName;
    public Text runeName;
    public Text runeDetails;

    PlayerSKillIDSO skill = null;
    [SerializeField]
    Button[] buttons;
    [SerializeField]
    Button mouseSkillFirst;
    [SerializeField]
    Button keyboardSkillFirst;

    void Start()
    {
        ShowMousePage();
    }

    void Update()
    {
        
    }

    public void ShowMousePage()
    {
        title.text = "마우스 스킬";
        pages[1].SetActive(true);
        pages[0].SetActive(false);
        mouseSkillFirst.onClick.Invoke();
    }

    public void ShowKeyboardPage()
    {
        title.text = "키보드 스킬";
        pages[1].SetActive(false);
        pages[0].SetActive(true);
        keyboardSkillFirst.onClick.Invoke();
    }

    public void OnClickedSkill(PlayerSKillIDSO skill)
    {
        this.skill = skill;

        for(int i = 0; i < skill.Runes.Length; ++i)
        {
            buttons[i].GetComponent<Image>().sprite = skill.Runes[i].sprite;
        }
        skillName.text = skill.GetSkillName();
        buttons[0].onClick.Invoke();
        buttons[0].Select();
    }

    public void OnClickedRune(int index)
    {
        if (skill == null) return;
        runeName.text = skill.Runes[index].RuneName;
        runeDetails.text = skill.Runes[index].Details;
    }
}
