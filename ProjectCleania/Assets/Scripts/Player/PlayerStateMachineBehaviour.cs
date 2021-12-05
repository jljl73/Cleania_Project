using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Text;

public class PlayerStateMachineBehaviour : StateMachineBehaviour
{
    PlayerSkillController playerSkillController;

    // 모든 스킬 트리거 & 상태 To Hash
    Dictionary<int, int> idToParameterHash = new Dictionary<int, int>();
    Dictionary<int, int> idToStateHash = new Dictionary<int, int>();

    // 특정 상태 해시 코드
    readonly int deadStateHash = Animator.StringToHash("Dead");

    private void Awake()
    {
        PlayerSkillSO[] playerSkillSOs = Resources.LoadAll<PlayerSkillSO>("ScriptableObject/SkillTable/PlayerSkill");
        UploadIDToHash(playerSkillSOs);

        playerSkillController = FindObjectOfType<PlayerSkillController>();
    }

    void UploadIDToHash(PlayerSkillSO[] so)
    {
        StringBuilder sb = new StringBuilder();
        for (int i = 0; i < so.Length; i++)
        {
            // Parameter 업로드
            sb.Clear();
            sb.Append("Trigger");
            sb.Append(so[i].ID.ToString());
            idToParameterHash.Add(so[i].ID, Animator.StringToHash(sb.ToString()));

            // State 업로드
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
    }

    // OnStateUpdate is called before OnStateUpdate is called on any state inside this state machine
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateExit is called before OnStateExit is called on any state inside this state machine
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // 스킬 나올때 모든 스킬 트리거 & 이펙트 끄기
        foreach (int id in idToStateHash.Keys)
        {
            // 현재 애니메이터 상태 == 등록된 스킬 애니메이터 상태
            if (stateInfo.shortNameHash == idToStateHash[id])
            {
                // 모든 스킬 트리거 끈다
                TurnOffAllSkillTrigger(animator);

                if (stateInfo.shortNameHash == idToStateHash[1102])
                    playerSkillController.StopSkill(1102);
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
        playerSkillController.StopAllSkill();
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
