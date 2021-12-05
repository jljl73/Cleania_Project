using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStopper : StateMachineBehaviour
{
    PlayerSkillController playerSkillController;

    [SerializeField]
    int skillID;

    private void Awake()
    {
        playerSkillController = FindObjectOfType<PlayerSkillController>();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //if (stateInfo.IsName("SkillR Set"))
        //    playerSkillController.DeactivateAllSkill();
    }
}
