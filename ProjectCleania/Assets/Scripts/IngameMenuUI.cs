using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngameMenuUI : MonoBehaviour
{
	public GameObject btnGoBack;
	public GameObject btnUserSetting;

	public GameObject UserSettingUIManagerObj;
	private UserSettingUIManager userSettingUIManager;

    public void Awake()
    {
		userSettingUIManager = UserSettingUIManagerObj.GetComponent<UserSettingUIManager>();
	}

    public void OnClickButton(GameObject obj)
	{
		if (obj == btnGoBack)
		{
			userSettingUIManager.PopDownUI();
		}
		if (obj == btnUserSetting)
		{
			userSettingUIManager.PopUpUI();
		}
	}
}
