using System.Collections.Generic;
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





    // Loader

    [System.NonSerialized]
    static Dictionary<int, ItemSO> _dictionary = new Dictionary<int, ItemSO>();
    static bool _loadedAll = false;

    static public ItemSO Load(int id)
    {
        ItemSO item;
        if (!_dictionary.TryGetValue(id, out item) && _loadedAll == false)
        {
            item = Resources.Load<ItemSO>($"ScriptableObject/ItemTable/{id.ToString()}");
            _dictionary[id] = item;
        }

        return item;
    }

    static public void LoadAll()
    {
        if (_loadedAll == false)
        {
            ItemSO[] all = Resources.LoadAll<ItemSO>("ScriptableObject/ItemTable");

            foreach (ItemSO i in all)
                _dictionary[i.ID] = i;

            _loadedAll = true;
        }
    }

    static ItemSO[] _commonItemSO;
    static public ItemSO[] CommonItemSO
    {
        get
        {
            if(_commonItemSO == null)
            {
                LoadAll();
                List<ItemSO> commons = new List<ItemSO>();

                foreach(var kv in _dictionary)
                {
                    if (kv.Value.rank == enumRank.Common)
                        commons.Add(kv.Value);
                }

                _commonItemSO = commons.ToArray();
            }

            return _commonItemSO;
        }
    }

    static ItemSO[] _rareItemSO;
    static public ItemSO[] RareItemSO
    {
        get
        {
            if(_rareItemSO == null)
            {
                LoadAll();
                List<ItemSO> rares = new List<ItemSO>();

                foreach(var kv in _dictionary)
                {
                    if (kv.Value.rank == enumRank.Rare)
                        rares.Add(kv.Value);
                }

                _rareItemSO = rares.ToArray();
            }

            return _rareItemSO;
        }
    }

    static ItemSO[] _legendaryItemSO;
    static public ItemSO[] LegendaryItemSO
    {
        get
        {
            if(_legendaryItemSO == null)
            {
                LoadAll();
                List<ItemSO> legendaries = new List<ItemSO>();

                foreach(var kv in _dictionary)
                {
                    if (kv.Value.rank == enumRank.Legendary)
                        legendaries.Add(kv.Value);
                }

                _legendaryItemSO = legendaries.ToArray();
            }

            return _legendaryItemSO;
        }
    }
}
