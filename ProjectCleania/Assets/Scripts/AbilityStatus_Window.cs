using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityStatus_Window : MonoBehaviour
{
    public GameObject vulnerable;
    AbilityStatus ability;

    Text[] texts;

    // Start is called before the first frame update
    void Start()
    {
        texts = GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (vulnerable != null)
            ability = vulnerable.GetComponent<AbilityStatus>();
        else
            ability = null;


        if(ability != null)
        {
            for (Ability.Stat i = 0; i < Ability.Stat.EnumTotal && (int)i < texts.Length; ++i)
            {
                texts[(int)i].text = i.ToString() + " : " + ability[i];
            }

            if (texts.Length > (int)Ability.Stat.EnumTotal)
            {
                for (int i = (int)Ability.Stat.EnumTotal; i < texts.Length; ++i)
                {
                    switch (i)
                    {
                        case (int)Ability.Stat.EnumTotal:
                            texts[(int)i].text = "Total Damage" + " : " + ability.TotalDamage();
                            break;
                        case (int)Ability.Stat.EnumTotal + 1:
                            texts[(int)i].text = "HP" + " : " + ability.HP;
                            break;
                        case (int)Ability.Stat.EnumTotal + 2:
                            texts[(int)i].text = "MP" + " : " + ability.MP;
                            break;
                    }
                }
            }
        }
        else
        {
            for (int i = 0; i < texts.Length; ++i)
                texts[i].text = "n/a";
        }
    }
}
