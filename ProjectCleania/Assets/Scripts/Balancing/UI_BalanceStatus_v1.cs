using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text;

public class UI_BalanceStatus_v1 : MonoBehaviour
{
    [SerializeField]
    bool show = false;

    [Header("do not touch below")]

    [SerializeField]
    TMP_InputField inputPlayerLevel;
    [SerializeField]
    Button buttonFullHPMP;

    [SerializeField]
    TextMeshProUGUI DPS;
    StringBuilder dps = new StringBuilder();
    string dpsHead = "DPS : ";
    [SerializeField]
    TextMeshProUGUI Toughness;
    StringBuilder toughness = new StringBuilder();
    string toughnessHead = "Toughness : ";

    [SerializeField]
    TMP_InputField inputEquipmentLevel;
    [SerializeField]
    TMP_Dropdown dropdownEquipment;
    [SerializeField]
    Button buttonRemoveAll;

    AbilityStatus playerAbility;
    Status playerStatus;
    ItemStorage_LocalGrid inventory;

    private void Awake()
    {
        GameManager.Instance.cheatWindow = this;   
    }

    // Start is called before the first frame update
    void Start()
    {
        playerAbility = GameManager.Instance.PlayerAbility;
        playerStatus = GameManager.Instance.PlayerStatus;
        inventory = SavedData.Instance.Item_Inventory;

        if (show)
            gameObject.SetActive(true);
        else
            gameObject.SetActive(false);
    }

    private void Update()
    {
        dps.Clear();
        dps.Append(dpsHead);
        dps.Append(playerAbility.DPS());
        DPS.text = dps.ToString();

        toughness.Clear();
        toughness.Append(toughnessHead);
        toughness.Append(playerAbility.Toughness());
        Toughness.text = toughness.ToString();
    }

    private void OnDestroy()
    {
        if (GameManager.Exists() && GameManager.Instance.cheatWindow == this)
            GameManager.Instance.cheatWindow = null;
    }

    public void OnPlayerLevelChanged()
    {
        int level = 1;
        int.TryParse(inputPlayerLevel.text, out level);

        if (level > 50)
            level = 50;
        else if (level < 1)
            level = 1;

        playerStatus.Level = level;
    }

    public void OnFullHPMP()
    {
        playerAbility.FullHP();
        playerAbility.FullMP();
    }

    public void OnGenerateEquipment()
    {
        ItemInstance_Equipment newEquipment;

        int level = 1;
        int.TryParse(inputEquipmentLevel.text, out level);

        if (level > 50)
            level = 50;
        else if (level < 1)
            level = 1;

        switch(dropdownEquipment.value)
        {
            case 0:     // main weapon
                newEquipment = ItemInstance_Equipment.Instantiate(1101001, level);
                break;
            case 1:     // hat
                newEquipment = ItemInstance_Equipment.Instantiate(1301001, level);
                break;
            case 2:     // chest
                newEquipment = ItemInstance_Equipment.Instantiate(1302001, level);
                break;
            case 3:     // pants
                newEquipment = ItemInstance_Equipment.Instantiate(1303001, level);
                break;
            case 4:     // glove
                newEquipment = ItemInstance_Equipment.Instantiate(1304001, level);
                break;
            case 5:     // shoes
                newEquipment = ItemInstance_Equipment.Instantiate(1305001, level);
                break;
            case 6:
                newEquipment = ItemInstance_Equipment.Instantiate(1101001, level);
                inventory.Add((ItemInstance)newEquipment);
                newEquipment = ItemInstance_Equipment.Instantiate(1301001, level);
                inventory.Add((ItemInstance)newEquipment);
                newEquipment = ItemInstance_Equipment.Instantiate(1302001, level);
                inventory.Add((ItemInstance)newEquipment);
                newEquipment = ItemInstance_Equipment.Instantiate(1303001, level);
                inventory.Add((ItemInstance)newEquipment);
                newEquipment = ItemInstance_Equipment.Instantiate(1304001, level);
                inventory.Add((ItemInstance)newEquipment);
                newEquipment = ItemInstance_Equipment.Instantiate(1305001, level);
                break;
            case 7: // insane weapon
                newEquipment = ItemInstance_Equipment.Instantiate(1101299, 50);
                inventory.Add((ItemInstance)newEquipment);
                newEquipment = ItemInstance_Equipment.Instantiate(1305299, 50);
                break;
            default:
                return;
        }

        inventory.Add((ItemInstance)newEquipment);
    }

    public void OnRemoveAll()
    {
        foreach(var i in inventory.Items)
        {
            inventory.Remove(i.Value);
        }
    }
}
