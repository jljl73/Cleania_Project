using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using LitJson;
using System.Text;

[System.Serializable]
public class EquipmentOption
{
    public enum Option
    {
        None,                   // ����
        Attack,                 // ������ݷ�
        AttackSpeed,            // ���ݼӵ�
        Strength,               // �ֽ���(��)
        IncreaseAttack,         // ���ݷ� ����
        CriticalChance,         // ġ��ŸȮ��
        CriticalScale,          // ġ��Ÿ���ط�
        Accuracy,               // ���߷�
        IncreaseDamage,         // ���� ����
        LifePerHit,             // Ÿ�ݽ� �����ȸ��
        LifePerKill,            // óġ�� �����ȸ��
        LifePerSecond,          // �ʴ� ����� ȸ����
        Vitality,               // �����
        MaxHP,                  // ü��
        MaxMP,                  // �ִ� �����ڿ�
        MPRestore,              // �����ڿ� ȹ�淮 ���� �̸� ����
        DamageIncreasedNormal,  // �Ϲݴ����������
        DamageIncreasedElite,   // ����Ʈ�����������
        DamageIncreasedBoss,    // ���������������
        SkillCoolDown,          // ������ð�����
        ExpIncreased,           // ����ġ ȹ�淮 ����
        CleanIncreased,         // Ŭ�� ȹ�淮 ����
        Defense,                // ����
        ReduceDamaged,          // ���� ����
        Tenacity,               // ������
        Dodge,                  // ȸ����
        MoveSpeed,              // �̵��ӵ�
    };

    public int ItemID;
    public int Level = 1;
    public Item.ITEMRANK Rank;
    public Item.ITEMSMALLCATEGORY type;
    public List<Option> StaticOptionKeys = new List<Option>();
    public List<int> StaticOptionValues = new List<int>();
    public List<Option> VariableOptionKeys = new List<Option>();
    public List<int> VariableOptionValues = new List<int>();

    Dictionary<Option, int> StaticOptions = new Dictionary<Option, int>();
    Dictionary<Option, int> VariableOptions = new Dictionary<Option, int>();

    public int this[Option option]
    {
        get
        {
            if (StaticOptions.TryGetValue(option, out int Value))
                return Value;
            else
                return 0;
        }
    }

    public EquipmentOption(Item.ITEMSMALLCATEGORY type, Item.ITEMRANK rank, int itemID, int level)
    {
        this.ItemID = itemID;
        this.type = type;
        this.Level = level;
        this.Rank = rank;
        Initialize();
    }

    public void Initialize()
    {
        int iRank = (int)this.Rank;
        System.Array EnumList = System.Enum.GetValues(typeof(EquipmentOption.Option));
        List<EquipmentOption.Option> options = EnumList.OfType<EquipmentOption.Option>().ToList();

        switch (this.type)
        {
            case Item.ITEMSMALLCATEGORY.Weapon: // ����
                StaticOptions.Add(Option.Attack, Random.Range(1250, 1651));
                StaticOptions.Add(Option.Strength, Random.Range(650, 751));
                SetRandomVariableOptions(options, 2 + iRank);
                break;
            case Item.ITEMSMALLCATEGORY.Hat: // �Ӹ�
                StaticOptions.Add(Option.Defense, Random.Range(650, 751));
                StaticOptions.Add(Option.Strength, Random.Range(650, 751));
                SetRandomVariableOptions(options, 1 + iRank);
                break;
            case Item.ITEMSMALLCATEGORY.Chest: // ����
                StaticOptions.Add(Option.Defense, Random.Range(650, 751));
                StaticOptions.Add(Option.Strength, Random.Range(400, 501));
                StaticOptions.Add(Option.MaxHP, Random.Range(400, 501));
                SetRandomVariableOptions(options, iRank);
                break;
            case Item.ITEMSMALLCATEGORY.Pants: // �ٸ�
                StaticOptions.Add(Option.Defense, Random.Range(650, 751));
                StaticOptions.Add(Option.Strength, Random.Range(400, 501));
                SetRandomVariableOptions(options, iRank);
                break;
            case Item.ITEMSMALLCATEGORY.Hands: // ��
                StaticOptions.Add(Option.Defense, Random.Range(500, 601));
                StaticOptions.Add(Option.Strength, Random.Range(650, 751));
                StaticOptions.Add(Option.CriticalChance, Random.Range(10, 16));
                SetRandomVariableOptions(options, 1 + iRank);
                break;
            case Item.ITEMSMALLCATEGORY.Boots: // �Ź�
                StaticOptions.Add(Option.Defense, Random.Range(500, 601));
                StaticOptions.Add(Option.Strength, Random.Range(400, 501));
                StaticOptions.Add(Option.MoveSpeed, Random.Range(10, 16));
                SetRandomVariableOptions(options, iRank);
                break;
        }

        foreach (KeyValuePair<Option, int> x in StaticOptions)
        {
            StaticOptionKeys.Add(x.Key);
            StaticOptionValues.Add(x.Value);
        }
        foreach (KeyValuePair<Option, int> x in VariableOptions)
        {
            VariableOptionKeys.Add(x.Key);
            VariableOptionValues.Add(x.Value);
        }
    }

    public void SetRandomVariableOptions(List<Option> options, int n)
    {
        if (options.Count < n) return;
        System.Random random = new System.Random();
        var shuffleOption = options.OrderBy(x => (random.Next())).ToList();

        int count = 0;
        for (int i = 0; i < options.Count; ++i)
        {
            Debug.Log(shuffleOption[i]);
            int value = GetOptionValue(this.type, shuffleOption[i]);
            if (value > 0)
            {
                count++;
                VariableOptions.Add(shuffleOption[i], GetOptionValue(this.type, shuffleOption[i]));
            }
            if (count == n) break;
        }
    }

    int GetOptionValue(Item.ITEMSMALLCATEGORY type, Option option)
    {
        int value = 0;

        switch (type)
        {
            case Item.ITEMSMALLCATEGORY.Weapon:
                switch (option)
                {
                    case Option.IncreaseAttack: value = Random.Range(10, 16); break;
                    case Option.CriticalChance: value = Random.Range(10, 16); break;
                    case Option.CriticalScale: value = Random.Range(40, 51); break;
                    case Option.Accuracy: value = Random.Range(10, 21); break;
                    case Option.LifePerHit: value = Random.Range(2000, 4001); break;
                    case Option.LifePerKill: value = Random.Range(10000, 20001); break;
                    case Option.MaxHP: value = Random.Range(650, 750); break;
                    case Option.MaxMP: value = 10; break;
                    case Option.MPRestore: value = Random.Range(10, 21); break;
                    default: value = 0; break;
                }
                break;
            case Item.ITEMSMALLCATEGORY.Hat:
                switch (option)
                {
                    case Option.IncreaseAttack: value = Random.Range(2, 8); break;
                    case Option.MaxHP: value = Random.Range(650, 751); break;
                    case Option.MPRestore: value = 10; break;
                    case Option.SkillCoolDown: value = Random.Range(15, 21); break;
                    case Option.ExpIncreased: value = Random.Range(20, 41); break;
                    case Option.CleanIncreased: value = Random.Range(20, 41); break;
                    case Option.Defense: value = Random.Range(500, 601); break;
                }
                break;
            case Item.ITEMSMALLCATEGORY.Chest:
                {
                    switch (option)
                    {
                        case Option.Defense: value = Random.Range(650, 751); break;
                        case Option.ReduceDamaged: value = Random.Range(7, 13); break;
                        case Option.Vitality: value = Random.Range(10, 16); break;
                        case Option.LifePerHit: value = Random.Range(2000, 4001); break;
                        case Option.LifePerKill: value = Random.Range(10000, 20001); break;
                        case Option.CleanIncreased: value = Random.Range(20, 41); break;
                        case Option.ExpIncreased: value = Random.Range(20, 41); break;
                        case Option.MaxMP: value = Random.Range(10, 21); break;
                    }
                }
                break;
            case Item.ITEMSMALLCATEGORY.Pants:
                switch (option)
                {
                    case Option.CriticalScale: value = Random.Range(10, 21); break;
                    case Option.Tenacity: value = Random.Range(30, 41); break;
                    case Option.MaxHP: value = Random.Range(400, 501); break;
                    case Option.Vitality: value = Random.Range(10, 16); break;
                    case Option.LifePerHit: value = Random.Range(2000, 4001); break;
                    case Option.LifePerKill: value = Random.Range(10000, 20001); break;
                    case Option.CleanIncreased: value = Random.Range(20, 41); break;
                    case Option.ExpIncreased: value = Random.Range(20, 41); break;
                    case Option.MaxMP: value = Random.Range(10, 21); break;
                    case Option.Dodge: value = Random.Range(5, 11); break;
                }
                break;
            case Item.ITEMSMALLCATEGORY.Hands:
                {
                    switch (option)
                    {
                        case Option.IncreaseAttack: value = Random.Range(10, 21); break;
                        case Option.Accuracy: value = Random.Range(10, 21); break;
                        case Option.CriticalScale: value = Random.Range(10, 21); break;
                        case Option.MaxHP: value = Random.Range(650, 751); break;
                        case Option.LifePerHit: value = Random.Range(2000, 4001); break;
                        case Option.LifePerKill: value = Random.Range(10000, 20001); break;
                        case Option.CleanIncreased: value = Random.Range(20, 41); break;
                        case Option.ExpIncreased: value = Random.Range(20, 41); break;
                        case Option.MaxMP: value = Random.Range(10, 21); break;
                    }
                }
                break;
            case Item.ITEMSMALLCATEGORY.Boots:
                {
                    switch (option)
                    {
                        case Option.MaxHP: value = Random.Range(400, 501); break;
                        case Option.Dodge: value = Random.Range(5, 11); break;
                        case Option.Tenacity: value = Random.Range(30, 41); break;
                        case Option.LifePerHit: value = Random.Range(2000, 4001); break;
                        case Option.LifePerKill: value = Random.Range(10000, 20001); break;
                        case Option.CleanIncreased: value = Random.Range(20, 41); break;
                        case Option.ExpIncreased: value = Random.Range(20, 41); break;
                        case Option.MaxMP: value = Random.Range(10, 21); break;
                    }
                }
                break;
        }

        return value;
    }

    public EquipmentOption DeepCopy()
    {
        EquipmentOption newOption = new EquipmentOption(this.type, this.Rank, this.ItemID, this.Level);
        newOption.StaticOptionKeys = this.StaticOptionKeys.ToList();
        newOption.StaticOptionValues = this.StaticOptionValues.ToList();
        newOption.VariableOptionKeys = this.VariableOptionKeys.ToList();
        newOption.VariableOptionValues = this.VariableOptionValues.ToList();

        return newOption;
    }
}
