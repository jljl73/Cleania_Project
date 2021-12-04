using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class PlayerStateMachineBehaviour : StateMachineBehaviour
{
    Dictionary<int, int> idToParameterHash = new Dictionary<int, int>();
    Dictionary<int, int> idToStateHash = new Dictionary<int, int>();
 
    private void Awake()
    {
        PlayerSkillSO[] playerSkillSOs = Resources.LoadAll<PlayerSkillSO>("ScriptableObject/SkillTable/PlayerSkill");
        UploadIDToHash(playerSkillSOs);
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
    //override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // ��ų ���ö� ��� ��ų Ʈ���� & ����Ʈ ����
        foreach (int id in idToStateHash.Keys)
        {
            // ���� �ִϸ����� ���� == ��ϵ� ��ų �ִϸ����� ����
            if (stateInfo.shortNameHash == idToStateHash[id])
            {
                // ��� ��ų ���� ����
                TurnOffAllSkillTrigger(animator);
                TurnOffAllSkillEffect();
                //animator.GetCurrentAnimatorClipInfo(0)[0].clip.events[0].
                break;
            }
        }
    }

    void TurnOffAllSkillTrigger(Animator animator)
    {
        foreach (int paramId in idToParameterHash.Keys)
        {
            animator.SetBool(idToParameterHash[paramId], false);
        }
    }

    void TurnOffAllSkillEffect()
    {

    }

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
