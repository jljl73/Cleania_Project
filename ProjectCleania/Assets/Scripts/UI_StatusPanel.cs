using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatusPanel : MonoBehaviour
{
     Status status;
     AbilityStatus abilityStatus;
    public GameObject Player;

    public Button detailBtn;
    public Text[] coreTexts;
    GameObject detailScroll;
    public Text[] detailTexts;

    private void Awake()
    {
        status = Player.GetComponent<Status>();
        abilityStatus = Player.GetComponent<AbilityStatus>();
        detailScroll = transform.Find("Detail Scroll").gameObject;
        //detailTexts = detailScroll.GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (status)
        {
            coreTexts[0].text = $"Level : {status.level.ToString().PadLeft(8)}";
        }

        if (abilityStatus)
        {
            coreTexts[0].text = $"{status.level}";
            coreTexts[1].text = $"{abilityStatus[Ability.Stat.Strength]}";
            coreTexts[2].text = $"{abilityStatus.DPS()}";
            coreTexts[3].text = $"{abilityStatus[Ability.Stat.Vitality]}";
            coreTexts[4].text = $"{abilityStatus[Ability.Stat.Defense]}";

            if (detailScroll.activeSelf)
            {
                detailTexts[0].text = $"{abilityStatus.ToString(Ability.Stat.Strength)}";
                detailTexts[1].text = $"{abilityStatus.ToString(Ability.Stat.Attack)}";
                detailTexts[2].text = $"{abilityStatus.ToString(Ability.Stat.CriticalChance)}";
                detailTexts[3].text = $"{abilityStatus.ToString(Ability.Stat.CriticalScale)}";
                detailTexts[4].text = $"{abilityStatus.ToString(Ability.Stat.AttackSpeed)}";
                detailTexts[5].text = $"{abilityStatus.ToString(Ability.Stat.Accuracy)}";
                detailTexts[6].text = $"{abilityStatus.ToString(Ability.Stat.IncreaseDamage)}";

                detailTexts[7].text = $"{abilityStatus.ToString(Ability.Stat.Vitality)}";
                detailTexts[8].text = $"{abilityStatus.ToString(Ability.Stat.MaxMP)}";
                detailTexts[9].text = $"{abilityStatus.ToString(Ability.Stat.Dodge)}";
                detailTexts[10].text = $"{abilityStatus.ToString(Ability.Stat.Tenacity)}";
                detailTexts[11].text = $"{abilityStatus.ToString(Ability.Stat.Defense)}";
                detailTexts[12].text = $"{abilityStatus.ToString(Ability.Stat.ReduceDamage)}";
                detailTexts[13].text = $"{abilityStatus.ToString(Ability.Stat.SkillCooldown)}";

                detailTexts[14].text = $"{abilityStatus.ToString(Ability.Stat.MoveSpeed)}";

                //for (int i = detailTexts.Length - 1; i >= 0; i--)
                //{
                    
                //    detailTexts[i].text = $"{abilityStatus[(Ability.Stat)i].ToString()}";
                //}
            }
        }

    }

    public void OnDetailBtn()
    {
        detailScroll.SetActive(!detailScroll.activeSelf);
    }
}
