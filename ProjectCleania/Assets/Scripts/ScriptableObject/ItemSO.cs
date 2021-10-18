using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/Item")]
public class ItemSO : ScriptableObject
{
    public enum enumMainCategory
    {
        Equipment = 1,
        Material = 2,
        Etc = 3
    }

    public enum enumSubCategory
    {
        Broom = 101,
        Head = 301,
        Chest = 302,
        Leg = 303,
        Hand = 304,
        Foot = 305,
        Jewwl = 777,

        Quest = 666,

        Null = 999,
    }

    public enum enumRank
    {
        Common = 0,
        Rare = 1,
        Legendary = 2
    }

    [SerializeField]
    enumMainCategory mainCategory;
    public enumMainCategory MainCategory
    { get => mainCategory; }
    
    [SerializeField]
    enumSubCategory subCategory;
    public enumSubCategory SubCategory
    { get => subCategory; }
    
    [SerializeField]
    enumRank rank;
    public enumRank Rank
    { get => rank; }

    [Range(0, 99), SerializeField]
    int customID;
    public int CustomID
    { get => customID; }

    public int ID
    {
        get
        {
            return 
                (int)mainCategory * 1000000 +
                (int)subCategory * 1000 +
                (int)rank * 100 +
                customID * 1;
        }
    }

    [SerializeField]
    string itemName;
    public string ItemName
    { get => itemName; }

    [SerializeField]
    Sprite itemImage;
    public Sprite ItemImage
    { get => itemImage; }

    //int maxDurability;
    //Equipment Option;

    [SerializeField]
    string toolTip;
    public string ToolTip
    { get => toolTip; }

    [SerializeField]
    int gridSizeX;
    [SerializeField]
    int gridSizeY;
    public Size GridSize
    { get => new Size(gridSizeX, gridSizeY); }

    [SerializeField]
    bool disassemble;
    public bool Disassemble
    { get => disassemble; }

    [SerializeField]
    bool tradable;
    public bool Tradable
    { get => tradable; }

    [SerializeField]
    int price;
    public int Price
    { get => price; }

    [SerializeField]
    bool droppable;
    public bool Droppable
    { get => droppable; }

    [SerializeField]
    EquipmentOptionSO optionTable;
    public EquipmentOptionSO OptionTable
    { get => optionTable; }


    static public ItemSO Load(int id)
    {
        return Resources.Load<ItemSO>($"ScriptableObject/ItemTable/{id.ToString()}");
    }
}
