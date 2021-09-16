using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CombatUI : MonoBehaviour
{
    public Image HP_Ball;
    public Image MP_Ball;
    public Image XP_Bar;
    public Image P1Portrait;

    [Range(0.0f, 1.0f)]
    public float HP_BallPercent = 1.0f;
    [Range(0.0f, 1.0f)]
    public float MP_BallPercent = 1.0f;
    [Range(0.0f, 1.0f)]
    public float XP_BarPercent = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HP_Ball.fillAmount = HP_BallPercent;
        MP_Ball.fillAmount = MP_BallPercent;
        XP_Bar.fillAmount = XP_BarPercent;
    }
}
