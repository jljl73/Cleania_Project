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

    [Range(0.0f, 1.0f)]
    public float HP_BallPercent = 1.0f;
    [Range(0.0f, 1.0f)]
    public float MP_BallPercent = 1.0f;
    [Range(0.0f, 1.0f)]
    public float XP_BarPercent = 1.0f;
    [Range(0.0f, 1.0f)]
    public float[] Skills_CoolPercent;

    // Start is called before the first frame update
    void Start()
    {
        Skills_CoolPercent = new float[Skills.Length];
    }

    // Update is called once per frame
    void Update()
    {
        HP_Ball.fillAmount = HP_BallPercent;
        P1Portrait.fillAmount = HP_BallPercent;
        MP_Ball.fillAmount = MP_BallPercent;
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
