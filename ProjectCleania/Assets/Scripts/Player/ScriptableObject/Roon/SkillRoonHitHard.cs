using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SkillRoonHitHard", menuName = "Scriptable Object/PlayerSkill/Roon/SkillRoonHitHard")]
public class SkillRoonHitHard : ScriptableObject
{
    public string Name;
    public string GetName() { return Name; }

    [Header("Tip: �������� �Է��� �� �ֽ��ϴ�.")]
    [TextArea]
    public string Details;

    public string GetDetails()
    {
        string tempString = Details;

        return tempString;
    }

    [Header("���ط� ������")]
    public float DamageScaleIncreaseRate;
    public float GetDamageScaleIncreaseRate() { return DamageScaleIncreaseRate; }
}
