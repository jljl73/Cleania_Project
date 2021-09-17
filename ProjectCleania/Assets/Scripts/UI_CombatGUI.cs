using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CombatGUI : MonoBehaviour
{
    public Image HP_Ball;
    public Image MP_Ball;
    public Image XP_Bar;
    public Image P1Portrait;
    public Image[] Skills;
    public GameObject HugeMap;

    public GameObject player;
    AbilityStatus playerStatus;

    [Range(0.0f, 1.0f)]
    public float XP_BarPercent = 1.0f;
    [Range(0.0f, 1.0f)]
    public float[] Skills_CoolPercent;

    // Start is called before the first frame update
    void Start()
    {
        Skills_CoolPercent = new float[Skills.Length];
        playerStatus = player.GetComponent<AbilityStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerStatus != null)
        {
            HP_Ball.fillAmount = playerStatus.HP / playerStatus[Ability.Stat.MaxHP];
            P1Portrait.fillAmount = HP_Ball.fillAmount;
            MP_Ball.fillAmount = playerStatus.MP / playerStatus[Ability.Stat.MaxMP];
        }
        XP_Bar.fillAmount = XP_BarPercent;
        for(int i = 0; i < Skills.Length; ++i)
        {
            Skills[i].fillAmount = Skills_CoolPercent[i];

            if (Skills[i].fillAmount < 1.0f)
                Skills[i].color = Color.gray;
            else
                Skills[i].color = Color.white;
        }

        if (Input.GetKeyDown(KeyCode.M))
            HugeMap.SetActive(!HugeMap.activeSelf);
    }
}
