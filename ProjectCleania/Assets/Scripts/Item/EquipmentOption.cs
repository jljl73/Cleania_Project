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
        None,                   // 없음
        Attack,                 // 무기공격력
        AttackSpeed,            // 공격속도
        Strength,               // 주스탯(힘)
        IncreaseAttack,         // 공격력 증가
        CriticalChance,         // 치명타확률
        CriticalScale,          // 치명타피해량
        Accuracy,               // 적중률
        IncreaseDamage,         // 피해 증가
        LifePerHit,             // 타격시 생명력회복
        LifePerKill,            // 처치시 생명력회복
        LifePerSecond,          // 초당 생명력 회복량
        Vitality,               // 생명력
        MaxHP,                  // 체력
        MaxMP,                  // 최대 고유자원
        MPRestore,              // 고유자원 획득량 증가 이름 미정
        DamageIncreasedNormal,  // 일반대상피해증가
        DamageIncreasedElite,   // 엘리트대상피해증가
        DamageIncreasedBoss,    // 보스대상피해증가
        SkillCoolDown,          // 재사용대기시간감소
        ExpIncreased,           // 경험치 획득량 증가
        CleanIncreased,         // 클린 획득량 증가
        Defense,                // 방어력
        ReduceDamaged,          // 피해 감소
        Tenacity,               // 강인함
        Dodge,                  // 회피율
        MoveSpeed,              // 이동속도
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
    }

    public void RandomInitialize()
    {
        int iRank = (int)this.Rank;
        System.Array EnumList = System.Enum.GetValues(typeof(EquipmentOption.Option));
        List<EquipmentOption.Option> options = EnumList.OfType<EquipmentOption.Option>().ToList();

        switch (this.type)
        {
            case Item.ITEMSMALLCATEGORY.Weapon: // 무기
                StaticOptions.Add(Option.Attack, Random.Range(1250, 1651));
                StaticOptions.Add(Option.Strength, Random.Range(650, 751));
                SetRandomVariableOptions(options, 2 + iRank);
                break;
            case Item.ITEMSMALLCATEGORY.Hat: // 머리
                StaticOptions.Add(Option.Defense, Random.Range(650, 751));
                StaticOptions.Add(Option.Strength, Random.Range(650, 751));
                SetRandomVariableOptions(options, 1 + iRank);
                break;
            case Item.ITEMSMALLCATEGORY.Chest: // 가슴
                StaticOptions.Add(Option.Defense, Random.Range(650, 751));
                StaticOptions.Add(Option.Strength, Random.Range(400, 501));
                StaticOptions.Add(Option.MaxHP, Random.Range(400, 501));
                SetRandomVariableOptions(options, iRank);
                break;
            case Item.ITEMSMALLCATEGORY.Pants: // 다리
                StaticOptions.Add(Option.Defense, Random.Range(650, 751));
                StaticOptions.Add(Option.Strength, Random.Range(400, 501));
                SetRandomVariableOptions(options, iRank);
                break;
            case Item.ITEMSMALLCATEGORY.Hands: // 손
                StaticOptions.Add(Option.Defense, Random.Range(500, 601));
                StaticOptions.Add(Option.Strength, Random.Range(650, 751));
                StaticOptions.Add(Option.CriticalChance, Random.Range(10, 16));
                SetRandomVariableOptions(options, 1 + iRank);
                break;
            case Item.ITEMSMALLCATEGORY.Boots: // 신발
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

    public void Load(List<Option> StaticOptionKeys, List<int> StaticOptionValues, List<Option> VariableOptionKeys, List<int> VariableOptionValues)
    {
        this.StaticOptionKeys = StaticOptionKeys.ToList();
        this.StaticOptionValues = StaticOptionValues.ToList();
        this.VariableOptionKeys = VariableOptionKeys.ToList();
        this.VariableOptionValues = VariableOptionValues.ToList();
    }
}
