using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerSkillDehydration", menuName = "Scriptable Object/PlayerSkill/PlayerSkillDehydration")]
public class PlayerSkillDehydrationSO : ScriptableObject
{
    public string SkillName;
    [Header("Tip: 변수명을 입력할 수 있습니다.")]
    [TextArea]
    public string SkillDetails;
    public string GetSkillDetails()
    {
        string tempString = SkillDetails;

        string coolTime = CoolTime.ToString();
        tempString = tempString.Replace("CoolTime", coolTime);

        string createdMP = CreatedMP.ToString();
        tempString = tempString.Replace("CreatedMP", createdMP);

        string consumMP = ConsumMP.ToString();
        tempString = tempString.Replace("ConsumMP", consumMP);

        string damageRate = (DamageRate * 100).ToString();
        tempString = tempString.Replace("DamageRate", damageRate);

        return tempString;
    }

    [Header("작동 키")]
    public string TriggerKey;

    // public bool isAttacking;
    [Header("쿨타임")]
    public float CoolTime;  // 추후 private 처리
    public float GetCoolTime { get { return CoolTime; } }

    [Header("생성 고유 자원")]
    public float CreatedMP = 0f;

    [Header("소모 고유 자원")]
    public float ConsumMP = 0f;

    [Header("전체 애니메이션 배속")]
    public float speedMultiplier = 1.0f;

    [Header("내려치기 데미지 비율 (ex. 2.0 = 200% 데미지 적용)")]
    public float DamageRate = 3.0f;

    /*
      빙글빙글 돌며 이동하며, 이동 경로에 있는 모든 적에게 초당 300%만큼의 피해를 줍니다.
     */
}
