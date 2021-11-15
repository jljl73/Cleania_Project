using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillRoonHitHard", menuName = "Scriptable Object/PlayerSkill/Roon/SkillRoonHitHard")]
public class SkillRoonHitHard : ScriptableObject
{
    public string Name;
    public string GetName() { return Name; }

    [Header("Tip: 변수명을 입력할 수 있습니다.")]
    [TextArea]
    public string Details;

    public string GetDetails()
    {
        string tempString = Details;

        return tempString;
    }

    [Header("피해량 증가량")]
    public float DamageScaleIncreaseRate;
    public float GetDamageScaleIncreaseRate() { return DamageScaleIncreaseRate; }
}
