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
    Dictionary<string, int> skillSlotDependencyDict = new Dictionary<string, int>();
    float[] coolTimePassed = new float[6];
    bool[] skillAvailable = new bool[6];

    float[] CoolTimePassedRatio = new float[6];

    public float GetCoolTimePassedRatio(int idx) { return CoolTimePassedRatio[idx]; }

    SkillStorage skillStorage;

    void Awake()
    {
        player = transform.parent.GetComponent<Player>();
        abilityStatus = player.abilityStatus;
    }

    void Start()
    {
        skillStorage = transform.parent.GetComponentInChildren<SkillStorage>();

        SetskillSlotDependencyDict();

        SetDefaultSkillSetting();

        for (int i = 0; i < skills.Length; i++)
        {
            coolTimePassed[i] = 1f;
            skillAvailable[i] = true;
        }
    }

    void SetskillSlotDependencyDict()
    {
        skillSlotDependencyDict.Add("1", 0);
        skillSlotDependencyDict.Add("2", 1);
        skillSlotDependencyDict.Add("3", 2);
        skillSlotDependencyDict.Add("4", 3);
        skillSlotDependencyDict.Add("C", 4);
        skillSlotDependencyDict.Add("R", 5);
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

            if (skills[i].GetCoolTime() < 0.01f)
                CoolTimePassedRatio[i] = 1f;
            else
                CoolTimePassedRatio[i] = coolTimePassed[i] / skills[i].GetCoolTime();

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

        // MP가 없으면 실행 불가
        if (!abilityStatus.ConsumeMP(skills[index].GetConsumMP()))
            return;

        if (index != 3 && index != 5) player.stateMachine.Transition(StateMachine.enumState.Attacking);

        skills[index].AnimationActivate();
        ResetSkill(index);
    }

    public void ActivateSkillEffect(int index)
    {
        if (skills[index].effectController != null)
            skills[index].effectController.PlaySkillEffect();
    }

    public void DeactivateSkillEffect(int index)
    {
        if (skills[index].effectController != null)
            skills[index].effectController.StopSKillEffect();
    }

    public void ActivateSkill(int index)
    {
        if(index == 3) player.stateMachine.Transition(StateMachine.enumState.Attacking);

        skills[index].Activate();

        // R스킬 때문인 것으로 추정되지만, 스킬 사용하자마자 하는게 좋으므로 Obsolete
        // abilityStatus.ConsumeMP(skills[index].ConsumMP);
    }

    public void DeactivateSkill(int index)
    {
        player.stateMachine.Transition(StateMachine.enumState.Idle);
        skills[index].Deactivate();
    }


    // ---------------------------------------------------------------------------------------------- //
    //                                             New Code
    // ---------------------------------------------------------------------------------------------- //

    public void ChangeSkill(string skillSlot, PlayerSkill.SkillID skillNameEnum)
    {
        PlayerSkill playerSkill = skillStorage.GetSkill(skillNameEnum);
        if (playerSkill is PlayerSkillDehydration)
        {
            //print("playerSkill is PlayerSkillDehydration");
            //print("dependency: playerSkill.GetSkillSlotDependency(): " + playerSkill.GetSkillSlotDependency());
            //print("skillSlot: " + skillSlot);
        }




        if (playerSkill.GetSkillSlotDependency() != skillSlot)
            return;

        //print("skillSlot: " + skillSlot);
        //print("skillSlotDependencyDict[skillSlot]: " + skillSlotDependencyDict[skillSlot]);

        skills[skillSlotDependencyDict[skillSlot]] = playerSkill;
    }

    void SetDefaultSkillSetting()
    {
        ChangeSkill("1", PlayerSkill.SkillID.FairysWings);
        ChangeSkill("2", PlayerSkill.SkillID.Sweeping);
        ChangeSkill("3", PlayerSkill.SkillID.CleaningWind);
        ChangeSkill("4", PlayerSkill.SkillID.RefreshingLeapForward);
        ChangeSkill("C", PlayerSkill.SkillID.Dusting);
        ChangeSkill("R", PlayerSkill.SkillID.Dehydration);

        //for (int i = 0; i < skills.Length; i++)
        //{
        //    if (skills[i] == null)
        //        Debug.Log(string.Format("skills {0} == null", i));
        //    else
        //        Debug.Log(string.Format("skills {0} != null", i));
        //}
    }
}
