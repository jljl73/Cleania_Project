using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillRevive", menuName = "Scriptable Object/PlayerSkill/PlayerSkillRevive")]
public class PlayerSkillReviveSO : PlayerSkillSO
{
    public override string GetSkillDetails()
    {
        string tempString = SkillDetails;

        //string coolTime = CoolTime.ToString();
        //tempString = tempString.Replace("CoolTime", coolTime);

        

        return tempString;
    }

    //[Header("도약 이팩트 반경")]
    //public float JumpEffectSize = 1f;
    //public float GetJumpEffectSize() { return JumpEffectSize; }
}
