using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CombatUI_Skillframe : MonoBehaviour
{
    Image[] images = new Image[2];

    // Start is called before the first frame update
    void Start()
    {
        images = GetComponentsInParent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (images[1].fillAmount < 1.0f)
            images[0].sprite = Resources.Load<Sprite>("ability_frame");
        else
            images[0].sprite = Resources.Load<Sprite>("ability_frame_announcement");
    }
}
