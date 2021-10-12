using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSkillManager : MonoBehaviour
{
    Player player;
    //public StateMachine stateMachine;
    AbilityStatus abilityStatus;

    PlayerSkill[] skills = new PlayerSkill[6];
    float[] coolTimePassed = new float[6];
    bool[] skillAvailable = new bool[6];

    float[] CoolTimePassedRatio = new float[6];

    public float GetCoolTimePassedRatio(int idx) { return CoolTimePassedRatio[idx]; }

    SkillStorage skillStorage;

    void Awake()
    {
        player = transform.parent.GetComponent<Player>();
        abilityStatus = player.abilityStatus;

        skillStorage = transform.parent.GetComponentInChildren<SkillStorage>();
    }

    void Start()
    {
        SetDefaultSkillSetting();

        for (int i = 0; i < skills.Length; i++)
        {
            coolTimePassed[i] = 1f;
            skillAvailable[i] = true;
        }
    }

    void Update()
    {
        UpdateCoolTime();
    }

    void UpdateCoolTime()
    {
        for (int i = 0; i < coolTimePassed.Length; i++)
        {
            // 쿨타임 업데이트
            coolTimePassed[i] += Time.deltaTime;
            if (skills[i].GetCoolTime < 0.01f)
                CoolTimePassedRatio[i] = 1f;
            else
                CoolTimePassedRatio[i] = coolTimePassed[i] / skills[i].GetCoolTime;

            // 업데이트가 됬으면 스킬 가능 설정
            if (CoolTimePassedRatio[i] >= 1f)
            {
                CoolTimePassedRatio[i] = 1f;
                skillAvailable[i] = true;
            }
        }
    }

    void ResetSkill(int index)
    {
        coolTimePassed[index] = 0f;
        skillAvailable[index] = false;
    }
    
    public void InputListener(int index)
    {
        if (!skillAvailable[index]) return;

        if(index != 3 && index != 5) player.stateMachine.Transition(StateMachine.enumState.Attacking);

        skills[index].AnimationActivate();
        ResetSkill(index);
    }

    public void ActivateSkill(int index)
    {
        if(index == 3) player.stateMachine.Transition(StateMachine.enumState.Attacking);

        skills[index].Activate();
        abilityStatus.ConsumeMP(skills[index].ConsumMP);
    }

    public void DeactivateSkill(int index)
    {
        player.stateMachine.Transition(StateMachine.enumState.Idle);
        skills[index].Deactivate();
    }


    // ---------------------------------------------------------------------------------------------- //
    //                                             New Code
    // ---------------------------------------------------------------------------------------------- //

    public void ChangeSkill(int skillSlotIndex, PlayerSkill.SkillID skillNameEnum)
    {
        skills[skillSlotIndex] = skillStorage.GetSkill(skillNameEnum);
    }

    void SetDefaultSkillSetting()
    {
        ChangeSkill(0, PlayerSkill.SkillID.FairysWings);
        ChangeSkill(1, PlayerSkill.SkillID.Sweeping);
        ChangeSkill(2, PlayerSkill.SkillID.CleaningWind);
        ChangeSkill(3, PlayerSkill.SkillID.RefreshingLeapForward);
        ChangeSkill(4, PlayerSkill.SkillID.Dusting);
        ChangeSkill(5, PlayerSkill.SkillID.Dehydration);
    }
}
