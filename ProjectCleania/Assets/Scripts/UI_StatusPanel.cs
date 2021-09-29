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
            coreTexts[1].text = $"Strength : {abilityStatus[Ability.Stat.Strength].ToString().PadLeft(5)}";
            coreTexts[2].text = $"Vitality : {abilityStatus[Ability.Stat.Vitality].ToString().PadLeft(5)}";
            coreTexts[3].text = $"Armor : {abilityStatus[Ability.Stat.Defense].ToString().PadLeft(8)}";
            coreTexts[4].text = $"DPS : {abilityStatus.TotalDamage().ToString().PadLeft(10)}";

            if (detailScroll.activeSelf)
            {
                for (int i = detailTexts.Length - 1; i >= 0; i--)
                {
                    detailTexts[i].text = $"{abilityStatus[(Ability.Stat)(i/2)].ToString()}";
                    //detailTexts[i].text = $"{((Ability.Stat)(i/2+1)).ToString()}";
                }
            }
        }

    }

    public void OnDetailBtn()
    {
        detailScroll.SetActive(!detailScroll.activeSelf);
    }
}
