using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Text;

public class PlayerStateMachineBehaviour : StateMachineBehaviour
{
    PlayerSkillController playerSkillController;
    AbilityStatus abilityStatus;

    // ��� ��ų �Ķ���� & ���� To Hash
    Dictionary<int, int> idToParameterHash = new Dictionary<int, int>();
    Dictionary<int, int> idToStateHash = new Dictionary<int, int>();

    // Ư�� ���� �ؽ� �ڵ�
    readonly int deadStateHash = Animator.StringToHash("Dead");
    readonly int movableHash = Animator.StringToHash("Movable");

    private void Awake()
    {
        PlayerSkillSO[] playerSkillSOs = Resources.LoadAll<PlayerSkillSO>("ScriptableObject/SkillTable/PlayerSkill");
        UploadIDToHash(playerSkillSOs);

        playerSkillController = FindObjectOfType<PlayerSkillController>();
        abilityStatus = playerSkillController.gameObject.GetComponent<AbilityStatus>();
    }

    void UploadIDToHash(PlayerSkillSO[] so)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < so.Length; i++)
        {
            // Parameter ���ε�
            sb.Clear();
            sb.Append("Trigger");
            sb.Append(so[i].ID.ToString());
            idToParameterHash.Add(so[i].ID, Animator.StringToHash(sb.ToString()));

            // State ���ε�
            sb.Clear();
            sb.Append("Skill ");
            sb.Append(so[i].ID.ToString());
            idToStateHash.Add(so[i].ID, Animator.StringToHash(sb.ToString()));
        }
    }

    // OnStateEnter is called before OnStateEnter is called on any state inside this state machine
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.shortNameHash == deadStateHash)
            TurnOffAllSkillEffect();

        // ��ų ���·� ���� ��
        foreach (int id in idToStateHash.Keys)
        {
            // ���� �ִϸ����� ���� == ��ϵ� ��ų �ִϸ����� ����
            if (stateInfo.shortNameHash == idToStateHash[id])
            {
                SetMovableParameter(animator, id);
                // SetCoolTimeInitialize(animator, id);
            }
        }

        if (stateInfo.shortNameHash == idToStateHash[1102])
        {
            skill1102TimePassed = 0;
        }
    }

    float skill1102TimePassed = 0;

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.shortNameHash == idToStateHash[1102])
        {
            skill1102TimePassed += Time.deltaTime;
            if (skill1102TimePassed > 1)
            {
                skill1102TimePassed = 0;
                abilityStatus.ConsumeMP(playerSkillController.GetMpValue(1102));
            }
        }
    }

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ��ų ���ö� �Ķ���� ���� & ����Ʈ ����
        foreach (int id in idToStateHash.Keys)
        {
            // ���� �ִϸ����� ���� == ��ϵ� ��ų �ִϸ����� ����
            if (stateInfo.shortNameHash == idToStateHash[id])
            {
                // ������ �� �ִ� ���� ����
                animator.SetBool(movableHash, true);

                // ��� ��ų �Ķ���� ����
                TurnOffAllSkillParameter(animator);

                // 1102 ��ų�� ���� �� ��ų Stop
                if (stateInfo.shortNameHash == idToStateHash[1102])
                    playerSkillController.StopSkill(1102);
                break;
            }
        }
    }

    void TurnOffAllSkillParameter(Animator animator)
    {
        foreach (int paramId in idToParameterHash.Keys)
        {
            animator.SetBool(idToParameterHash[paramId], false);
        }
    }

    void TurnOffAllSkillEffect()
    {
        playerSkillController.StopAllSkill();
    }

    void SetMovableParameter(Animator animator, int id)
    {
        switch (id)
        {
            case 1102:
            case 1106:
            case 1198:
                break;
            default:
                // ������ �� ���� ���� ����
                animator.SetBool(movableHash, false);
                break;
        }
    }

    //void SetCoolTimeInitialize(Animator animator, int id)
    //{
    //    switch (id)
    //    {
    //        case 1199:
    //            if (!playerSkillController.AnimationActivate(id))
    //            {
    //                playerSkillController.ResetSkill(id);
    //                playerSkillController.StopSkill(id);
    //                animator.SetBool(idToParameterHash[id], false);
    //            }
    //            break;
    //        default:
    //            // ��ų ���� ������ �ִϸ��̼� ���� ���� ���¸�, ��Ÿ�� �ʱ�ȭ
    //            if (playerSkillController.AnimationActivate(id))
    //                playerSkillController.ResetSkill(id);
    //            else
    //                animator.SetBool(idToParameterHash[id], false);
    //            break;
    //    }
    //}

    // OnStateMove is called before OnStateMove is called on any state inside this state machine
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateIK is called before OnStateIK is called on any state inside this state machine
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMachineEnter is called when entering a state machine via its Entry Node
    //override public void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}

    // OnStateMachineExit is called when exiting a state machine via its Exit Node
    //override public void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    //{
    //    
    //}
}
