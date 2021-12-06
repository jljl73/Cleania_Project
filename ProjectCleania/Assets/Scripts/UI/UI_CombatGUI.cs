using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UI_CombatGUI : MonoBehaviour
{
    public Image HPBall;
    public Image HPSparkling;
    public Image MPBall;
    public Image MPSparkling;

    public Image XP_Bar;
    public Image P1Portrait;
    public Image[] Skills;
    public GameObject HugeMap;

    public Text Level;
    public Text CurrentTime;
    public Text LocationName;

    GameObject player;
    AbilityStatus playerStatus;

    [Range(0.0f, 1.0f)]
    public float XP_BarPercent = 1.0f;

    PlayerSkillController playerSkillController;
    [Range(0.0f, 1.0f)]
    public float[] Skills_CoolPercent;

    void Start()
    {
        player = GameManager.Instance.SinglePlayer;
        playerSkillController = player.GetComponent<PlayerSkillController>();

        Skills_CoolPercent = new float[Skills.Length];
        playerStatus = player.GetComponent<AbilityStatus>();

        LocationName.text = SceneManager.GetActiveScene().name;
    }

    void Update()
    {
        CurrentTime.text = DateTime.Now.ToString(("HH:mm:ss"));
        if (playerStatus != null)
        {
            // 체력 구슬
            HPBall.fillAmount = playerStatus.HP / playerStatus[Ability.Stat.MaxHP];

            Vector3 tempScale = new Vector3(0.25f + 0.75f * Mathf.Sin(Mathf.PI * HPBall.fillAmount), 0.25f + 0.75f * Mathf.Sin(Mathf.PI * HPBall.fillAmount), HPSparkling.rectTransform.localScale.z);
            Vector3 tempPosition = new Vector3(HPSparkling.rectTransform.localPosition.x, HPBall.rectTransform.sizeDelta.y * HPBall.fillAmount - HPBall.rectTransform.sizeDelta.y * 0.5f, HPSparkling.rectTransform.localPosition.z);
            // 스케일
            HPSparkling.rectTransform.localScale = tempScale;
            // 위치
            HPSparkling.rectTransform.localPosition = tempPosition;
            // <<

            // 마력 구슬
            MPBall.fillAmount = playerStatus.MP / playerStatus[Ability.Stat.MaxMP];
            
            tempScale = new Vector3(0.25f + 0.75f * Mathf.Sin(Mathf.PI * MPBall.fillAmount), 0.25f + 0.75f * Mathf.Sin(Mathf.PI * MPBall.fillAmount), MPSparkling.rectTransform.localScale.z);
            tempPosition = new Vector3(MPSparkling.rectTransform.localPosition.x, MPBall.rectTransform.sizeDelta.y * MPBall.fillAmount - MPBall.rectTransform.sizeDelta.y * 0.5f, MPSparkling.rectTransform.localPosition.z);
            // 스케일
            MPSparkling.rectTransform.localScale = tempScale;
            // 위치
            MPSparkling.rectTransform.localPosition = tempPosition;
            // <<
            
            // 초상화
            P1Portrait.fillAmount = HPBall.fillAmount;

            // 쿨타임 업데이트
            UpdateSkillCoolTime();
        }
        XP_Bar.fillAmount = ExpManager.Percent;
        Level.text = ExpManager.Level.ToString();
    }

    void UpdateSkillCoolTime()
    {
        Skills_CoolPercent[0] = playerSkillController.GetCoolTimePassedRatio(1103);
        Skills_CoolPercent[1] = playerSkillController.GetCoolTimePassedRatio(1104);
        Skills_CoolPercent[2] = playerSkillController.GetCoolTimePassedRatio(1105);
        Skills_CoolPercent[3] = playerSkillController.GetCoolTimePassedRatio(1106);
        Skills_CoolPercent[4] = playerSkillController.GetCoolTimePassedRatio(1101);
        Skills_CoolPercent[5] = playerSkillController.GetCoolTimePassedRatio(1102);

        Skills[0].fillAmount = Skills_CoolPercent[0];
        Skills[1].fillAmount = Skills_CoolPercent[1];
        Skills[2].fillAmount = Skills_CoolPercent[2];
        Skills[3].fillAmount = Skills_CoolPercent[3];
        Skills[4].fillAmount = Skills_CoolPercent[4];
        Skills[5].fillAmount = Skills_CoolPercent[5];
    }
}
