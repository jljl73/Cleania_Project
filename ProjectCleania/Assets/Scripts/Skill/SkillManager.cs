using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillManager : MonoBehaviour
{
    public StateMachine stateMachine;
    private AbilityStatus abilityStatus;
    public Skill[] skills;              // �ν����Ϳ��� �Ҵ�
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
        // ���� �ʱ�ȭ
        for (int i = 0; i < skills.Length; i++)
        {
            coolTimePassed[i] = 0;
            skillAvailable[i] = false;
        }
    }

    void Update()
    {
        //skill = InputHandler();
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SkillTry(0);
        else
            skill = null;

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
            // ��Ÿ�� ������Ʈ
            coolTimePassed[i] += Time.deltaTime;
            if (skills[i].GetCoolTime < 0.01f)
                CoolTimePassedRatio[i] = 1f;
            else
                CoolTimePassedRatio[i] = coolTimePassed[i] / (skills[i].GetCoolTime * cooldownSpeed);

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
