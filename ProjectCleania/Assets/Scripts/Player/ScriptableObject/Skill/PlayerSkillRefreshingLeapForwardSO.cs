using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillRefreshingLeapForward", menuName = "Scriptable Object/PlayerSkill/PlayerSkillRefreshingLeapForward")]
public class PlayerSkillRefreshingLeapForwardSO : PlayerSkillSO
{
    public override string GetSkillDetails()
    {
        string tempString = SkillDetails;

        string coolTime = CoolTime.ToString();
        tempString = tempString.Replace("CoolTime", coolTime);

        string createdMP = CreatedMP.ToString();
        tempString = tempString.Replace("CreatedMP", createdMP);

        string consumMP = ConsumMP.ToString();
        tempString = tempString.Replace("ConsumMP", consumMP);

        string smashDamageRate = (SmashDamageRate * 100).ToString();
        tempString = tempString.Replace("SmashDamageRate", smashDamageRate);

        string smashRange = SmashRange.ToString();
        tempString = tempString.Replace("SmashRange", smashRange);

        string stunTime = StunTime.ToString();
        tempString = tempString.Replace("StunTime", stunTime);

        string slowTime = SlowTime.ToString();
        tempString = tempString.Replace("SlowTime", slowTime);

        return tempString;
    }

    [Header("���� ����Ʈ �ݰ�")]
    public float JumpEffectSize = 1f;
    public float GetJumpEffectSize() { return JumpEffectSize; }

    [Header("���ڷ� �ֵθ� �ݰ�")]
    public float SwingDownSize = 1f;
    public float GetSwingDownSize() { return SwingDownSize; }

    [Header("����ġ�� ������ ���� (ex. 2.0 = 200% ������ ����)")]
    public float SmashDamageRate = 5.4f;
    public float GetSmashDamageRate() { return SmashDamageRate; }

    [Header("�� ������� �ݰ�")]
    public float SmashRange = 2f;
    public float GetSmashRange() { return SmashRange; }

    [Header("���� �ð�")]
    public float StunTime = 1.5f;
    public float GetStunTime() { return StunTime; }

    [Header("���ο� �ð�")]
    public float SlowTime = 2f;
    public float GetSlowTime() { return SlowTime; }

    [Header("���ڸ� ���� �Ÿ�")]
    public float JumpDistance = 7f;
    public float GetJumpDistance() { return JumpDistance; }

    [Header("�ִϸ��̼� �и� ����")]
    public int AnimationSplitCount = 1;
    public int GetAnimationSplitCount() { return AnimationSplitCount; }


    /*
     �ݿ��� ���¸� �׸��� �������� �پ���� �� ����� ������ �����ϸ�, 540%�� ���ظ� �ݴϴ�. �ǰݵ� ������ 1.5�ʰ� ���� ���¿� ���� ��, 2�ʰ� ���ο� ���°� �˴ϴ�.
     */
}
