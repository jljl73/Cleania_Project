using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public StateMachine stateMachine;
    private AbilityStatus abilityStatus;
    public Skill[] skills;              // 인스펙터에서 할당
    Skill skill;

    float[] coolTimePassed;
    bool[] skillAvailable;
    public float[] CoolTimePassedRatio;

    private void Awake()
    {
        abilityStatus = GetComponent<AbilityStatus>();
        coolTimePassed = new float[skills.Length];
        skillAvailable = new bool[skills.Length];
        CoolTimePassedRatio = new float[skills.Length];
    }

    private void Start()
    {
        // 셋팅 초기화
        for (int i = 0; i < skills.Length; i++)
        {
            coolTimePassed[i] = 0;
            skillAvailable[i] = false;
        }
    }

    void Update()
    {
        //skill = InputHandler();

        if (skill)
            skill.AnimationActivate();

        updateCoolTime();
    }

    public bool SkillTry(int index)
    {
        if(isSkillAvailable(index))
        {
            initializeSkillSetting(index);
            abilityStatus.ConsumeMP(skills[index].ConsumMP);
            skill = skills[index];
            return true;
        }
        else
        {
            skill = null;
            return false;
        }
    }

//    Skill InputHandler()
//    {

//        // 키보드
//        if (Input.GetKeyDown(KeyCode.Alpha1) && isSkillAvailable(0))
//        {
//            initializeSkillSetting(0);
//            abilityStatus.ConsumeMP(skills[0].ConsumMP);
//            return skills[0];
//        }
//        if (Input.GetKeyDown(KeyCode.Alpha2) && isSkillAvailable(1))
//        {
//            initializeSkillSetting(1);
//            abilityStatus.ConsumeMP(skills[1].ConsumMP);
//            return skills[1];
//        }
//        if (Input.GetKeyDown(KeyCode.Alpha3) && isSkillAvailable(2))
//        {
//            initializeSkillSetting(2);
//            abilityStatus.ConsumeMP(skills[2].ConsumMP);
//            return skills[2];
//        }
//        if (Input.GetKeyDown(KeyCode.Alpha4) && isSkillAvailable(3))
//        {
//            initializeSkillSetting(3);
//            abilityStatus.ConsumeMP(skills[3].ConsumMP);
//            return skills[3];
//        }

//        // 마우스
//        if (Input.GetKeyDown(KeyCode.C) && isSkillAvailable(4))
//        {
//            initializeSkillSetting(4);
//            abilityStatus.ConsumeMP(skills[4].ConsumMP);
//            return skills[4];
//        }
//        if (Input.GetMouseButtonDown(1) && (isSkillAvailable(5) ||
//            stateMachine.State == StateMachine.enumState.MoveAttack))
//        {
//#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
//            if (!EventSystem.current.IsPointerOverGameObject())
//            {
//                initializeSkillSetting(5);
//                abilityStatus.ConsumeMP(skills[5].ConsumMP);
//                return skills[5];
//            }
//#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
//            if (!EventSystem.current.IsPointerOverGameObject(0))
//            {
//                initializeSkillSetting(5);
//                playerAbilityStatus.ConsumeMP(skills[5].ConsumMP);
//                return skills[5];
//            }
//#endif
//        }

//        return null;
//    }

    public void ActivateSkill(int type)
    {
        skills[type].Activate();
    }

    public void AnimationDeactivate(int type)
    {
        skills[type].AnimationDeactivate();
        //playerStateMachine.Transition(StateMachine.enumState.Idle);
    }

    bool isSkillAvailable(int skillIndex)
    {
        if ((stateMachine.State == StateMachine.enumState.Idle ||
            stateMachine.State == StateMachine.enumState.Chasing) && skillAvailable[skillIndex])
            return true;
        else
            return false;
    }

    void updateCoolTime()
    {
        float cooldownSpeed = abilityStatus[Ability.Stat.SkillCooldown];

        for (int i = 0; i < coolTimePassed.Length; i++)
        {
            // 쿨타임 업데이트
            coolTimePassed[i] += Time.deltaTime;
            if (skills[i].GetCoolTime < 0.01f)
                CoolTimePassedRatio[i] = 1f;
            else
                CoolTimePassedRatio[i] = coolTimePassed[i] / (skills[i].GetCoolTime * cooldownSpeed);

            // 업데이트가 됬으면 스킬 가능 설정
            if (CoolTimePassedRatio[i] >= 1f)
            {
                CoolTimePassedRatio[i] = 1f;
                skillAvailable[i] = true;
            }
        }
    }

    void initializeSkillSetting(int index)
    {
        coolTimePassed[index] = 0f;
        skillAvailable[index] = false;
    }
}
