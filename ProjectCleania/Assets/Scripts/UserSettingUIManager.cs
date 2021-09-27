using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserSettingUIManager : MonoBehaviour
{
	public GameObject IngameMenuUI;
	public GameObject UserSettingUI;

	List<GameObject> uiList;
	int currentIndex = -1;

	public bool IsActive
	{
		get
		{
			foreach (GameObject ui in uiList)
			{
				if (ui.activeSelf) return true;
			}
			return false;
		}
	}

	void Awake()
	{
		DontDestroyOnLoad(this.gameObject);
	}

	void Start()
	{
		// UI ���� �� ���� �ֱ�
		uiList = new List<GameObject>();
		uiList.Add(IngameMenuUI);
		uiList.Add(UserSettingUI);

		// �ʱ� ���� active off
		foreach (GameObject ui in uiList)
		{
			ui.SetActive(false);
		}
		currentIndex = -1;
	}

	void Update()
	{
		// if (!GameManager.IsIngame) return;

		if (Input.GetKeyDown(KeyCode.Escape))
		{
			PopDownUI();
		}
	}

	public void PopDownUI()
	{
		if (currentIndex == -1) return;
		uiList[currentIndex].SetActive(false);
		currentIndex--;
	}

	public void PopUpUI()
	{
		if (currentIndex == (uiList.Count - 1)) return;
		currentIndex++;
		uiList[currentIndex].SetActive(true);
	}
}
