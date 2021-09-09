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
        ability = vulnerable.GetComponent<AbilityStatus>();
        texts = GetComponentsInChildren<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        for(Ability.Stat i = 0; i < Ability.Stat.EnumTotal || (int)i < texts.Length; ++i)
        {
            texts[(int)i].text = i.ToString() + " : " + ability[i];
        }
    }
}
