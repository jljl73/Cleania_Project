using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillStopper : StateMachineBehaviour
{
    PlayerSkillManager playerSkillManager;

    [SerializeField]
    int skillID;

    private void Awake()
    {
        playerSkillManager = FindObjectOfType<PlayerSkillManager>();
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (stateInfo.IsName("SkillR Set"))
            playerSkillManager.StopSkill(skillID);
    }
}
