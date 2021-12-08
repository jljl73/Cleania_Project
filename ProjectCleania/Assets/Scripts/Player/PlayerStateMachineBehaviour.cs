using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Text;

public class PlayerStateMachineBehaviour : StateMachineBehaviour
{
    PlayerSkillController playerSkillController;
    AbilityStatus abilityStatus;

    // 모든 스킬 파라미터 & 상태 To Hash
    Dictionary<int, int> idToParameterHash = new Dictionary<int, int>();
    Dictionary<int, int> idToStateHash = new Dictionary<int, int>();

    // 특정 상태 해시 코드
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

        // 스킬 상태로 들어올 때
        foreach (int id in idToStateHash.Keys)
        {
            // 현재 애니메이터 상태 == 등록된 스킬 애니메이터 상태
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
        // 스킬 나올때 파라미터 끄기 & 이펙트 끄기
        foreach (int id in idToStateHash.Keys)
        {
            // 현재 애니메이터 상태 == 등록된 스킬 애니메이터 상태
            if (stateInfo.shortNameHash == idToStateHash[id])
            {
                // 움직일 수 있는 상태 설정
                animator.SetBool(movableHash, true);

                // 모든 스킬 파라미터 끈다
                TurnOffAllSkillParameter(animator);

                // 1102 스킬은 나올 때 스킬 Stop
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
                // 움직일 수 없는 상태 설정
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
    //            // 스킬 내부 로직이 애니메이션 실행 가능 상태면, 쿨타임 초기화
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
