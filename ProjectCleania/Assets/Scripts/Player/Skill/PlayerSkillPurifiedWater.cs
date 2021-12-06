using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillPurifiedWater : PlayerSkill
{
    [SerializeField]
    PlayerSkillPurifiedWaterSO skillData;

    float HPRecoveryRate;
    float MPRecoveryRate;

    public override int ID { get { return skillData.ID; } protected set { id = value; } }

    private new void Awake()
    {
        base.Awake();
    }

    protected new void Start()
    {
        base.Start();
        UpdateskillData();
    }

    public void UpdateskillData()
    {
        base.UpdateSkillData(skillData);

        HPRecoveryRate = skillData.GetHPRecoveryRate();
        MPRecoveryRate = skillData.GetMPRecoveryRate();
    }

    public override bool IsAvailable()
    {
        return (int)OwnerAbilityStatus.HP != (int)OwnerAbilityStatus.GetStat(Ability.Stat.MaxHP);
    }

    public override bool AnimationActivate()
    {
        base.AnimationActivate();
        // 체력 100이면 사용 불가
        print("전체 체력의 60% 회복! 전체 코스트 30% 회복!");
        
        return true;
    }

    public override void ActivateSound(int index)
    {
        GameManager.Instance.playerSoundPlayer.PlaySound(PlayerSoundPlayer.TYPE.DrinkPotion);
    }
}
