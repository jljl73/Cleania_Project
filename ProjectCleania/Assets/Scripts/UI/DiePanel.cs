using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiePanel : MonoBehaviour
{
    [SerializeField]
    Button hereReviveButton;
    [SerializeField]
    Button villageReviveButton;

    private void Awake()
    {
        if (hereReviveButton == null)
            throw new System.Exception("DiePanel doesnt have hereReviveButton");
        if (villageReviveButton == null)
            throw new System.Exception("DiePanel doesnt have villageReviveButton");
    }

    void OnEnable()
    {
        Invoke("InteractableDelay", 3);
    }

    private void OnDisable()
    {
        hereReviveButton.interactable = false;
        villageReviveButton.interactable = false;
    }

    void InteractableDelay()
    {
        hereReviveButton.interactable = true;
        villageReviveButton.interactable = true;
    }

    public void HereRevive()
    {
        if (GameManager.Instance.SinglePlayer.GetComponent<PlayerController>().OrderSkillID(1194))
            gameObject.SetActive(false);
    }

    public void VillageRevive()
    {
        if (GameManager.Instance.SinglePlayer.GetComponent<PlayerController>().OrderSkillID(1195))
            gameObject.SetActive(false);
    }
}
