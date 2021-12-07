using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_CharacterSlot : MonoBehaviour
{
    public Text characterName;
    public Text characterLevel;

    public void OnClick()
    {
        SavedData.Instance.CharacterName = characterName.text;
    }
}
