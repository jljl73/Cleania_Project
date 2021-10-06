using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerSkillManager : MonoBehaviour
{
    public StateMachine playerStateMachine;
    private AbilityStatus playerAbilityStatus;
    public Skill[] skills = new Skill[6];
    Skill skill;

    float[] coolTimePassed = new float[6];
    bool[] skillAvailable = new bool[6];
    public float[] CoolTimePassedRatio = new float[6];

    private void Awake()
    {
        playerAbilityStatus = GetComponent<AbilityStatus>();
    }

    private void Start()
    {
        // ���� �ʱ�ȭ
        for (int i = 0; i < skills.Length; i++)
        {
            coolTimePassed[i] = 0;
            skillAvailable[i] = false;
        }
    }

    void Update()
    {
        skill = InputHandler();

        if (skill)
            skill.AnimationActivate();

        updateCoolTime();
    }

    Skill InputHandler()
    {

        // Ű����
        if (Input.GetKeyDown(KeyCode.Alpha1) && isSkillAvailable(0))
        {
            initializeSkillSetting(0);
            playerAbilityStatus.ConsumeMP(skills[0].ConsumMP);
            return skills[0];
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && isSkillAvailable(1))
        {
            initializeSkillSetting(1);
            playerAbilityStatus.ConsumeMP(skills[1].ConsumMP);
            return skills[1];
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && isSkillAvailable(2))
        {
            initializeSkillSetting(2);
            playerAbilityStatus.ConsumeMP(skills[2].ConsumMP);
            return skills[2];
        }
        if (Input.GetKeyDown(KeyCode.Alpha4) && isSkillAvailable(3))
        {
            initializeSkillSetting(3);
            playerAbilityStatus.ConsumeMP(skills[3].ConsumMP);
            return skills[3];
        }

        // ���콺
        if (Input.GetKeyDown(KeyCode.C) && isSkillAvailable(4))
        {
            initializeSkillSetting(4);
            playerAbilityStatus.ConsumeMP(skills[4].ConsumMP);
            return skills[4];
        }
        if (Input.GetMouseButtonDown(1) && (isSkillAvailable(5) ||
            playerStateMachine.State == StateMachine.enumState.MoveAttack))
        {
#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBPLAYER
            if (!EventSystem.current.IsPointerOverGameObject())
            {
                initializeSkillSetting(5);
                playerAbilityStatus.ConsumeMP(skills[5].ConsumMP);
                return skills[5];
            }
#elif UNITY_IOS || UNITY_ANDROID || UNITY_WP8 || UNITY_IPHONE
            if (!EventSystem.current.IsPointerOverGameObject(0))
            {
                initializeSkillSetting(5);
                playerAbilityStatus.ConsumeMP(skills[5].ConsumMP);
                return skills[5];
            }
#endif
        }

        return null;
    }

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
        if ((playerStateMachine.State == StateMachine.enumState.Idle ||
            playerStateMachine.State == StateMachine.enumState.Chasing) && skillAvailable[skillIndex])
            return true;
        else
            return false;
    }

    void updateCoolTime()
    {
        for (int i = 0; i < coolTimePassed.Length; i++)
        {
            // ��Ÿ�� ������Ʈ
            coolTimePassed[i] += Time.deltaTime;
            if (skills[i].GetCoolTime < 0.01f)
                CoolTimePassedRatio[i] = 1f;
            else
                CoolTimePassedRatio[i] = coolTimePassed[i] / skills[i].GetCoolTime;

            // ������Ʈ�� ������ ��ų ���� ����
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
