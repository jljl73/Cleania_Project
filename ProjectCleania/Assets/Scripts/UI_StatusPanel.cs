using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_StatusPanel : MonoBehaviour
{
     Status status;
     AbilityStatus abilityStatus;

    GameObject detailScroll;
    public Text[] coreTexts;
    public Text[] detailTexts;

    private void Awake()
    {
        abilityStatus = GameManager.Instance.PlayerAbility;
        status = GameManager.Instance.PlayerStatus;
        detailScroll = transform.Find("Detail Scroll").gameObject;
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
                detailTexts[0].text = abilityStatus.StatToString(Ability.Stat.Strength);
                detailTexts[1].text = abilityStatus.StatToString(Ability.Stat.Attack);
                detailTexts[2].text = abilityStatus.StatToString(Ability.Stat.CriticalChance);
                detailTexts[3].text = abilityStatus.StatToString(Ability.Stat.CriticalScale);
                detailTexts[4].text = abilityStatus.StatToString(Ability.Stat.AttackSpeed);
                detailTexts[5].text = abilityStatus.StatToString(Ability.Stat.Accuracy);
                detailTexts[6].text = abilityStatus.StatToString(Ability.Stat.IncreaseDamage);

                detailTexts[7].text = abilityStatus.StatToString(Ability.Stat.Vitality);
                detailTexts[8].text = abilityStatus.StatToString(Ability.Stat.MaxMP);
                detailTexts[9].text = abilityStatus.StatToString(Ability.Stat.Dodge);
                detailTexts[10].text = abilityStatus.StatToString(Ability.Stat.Tenacity);
                detailTexts[11].text = abilityStatus.StatToString(Ability.Stat.Defense);
                detailTexts[12].text = abilityStatus.StatToString(Ability.Stat.ReduceDamage);
                detailTexts[13].text = abilityStatus.StatToString(Ability.Stat.SkillCooldown);

                detailTexts[14].text = abilityStatus.StatToString(Ability.Stat.MoveSpeed);

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
