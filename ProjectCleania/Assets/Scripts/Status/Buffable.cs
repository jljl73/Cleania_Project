using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buffable : MonoBehaviour
{
    // ¹öÇÁÇü
    [SerializeField]
    float[] _buffOptions = { 1, 1, 1, 1, 1, 1 };
    int[] _buffOptionsOvelapped = { 0, 0, 0, 0, 0, 0 };

    public float this[Ability.Buff index]
    {
        get => _buffOptions[(int)index];
    }

    public void AddBuff(float value, Ability.Buff option, float duration)
    {
        if (!CheckBuffOvelapped(option))
            return;
        _buffOptions[(int)option] += value;
        _buffOptionsOvelapped[(int)option] += 1;
        StartCoroutine(OffBuff(value, option, duration));
        Debug.Log("add buff : " + option.ToString() + " : " + _buffOptions[(int)option]);
    }

    IEnumerator OffBuff(float value, Ability.Buff option, float duration)
    {
        yield return new WaitForSeconds(duration);
        _buffOptions[(int)option] -= value;
        _buffOptionsOvelapped[(int)option] -= 1;
        Debug.Log("off buff : " + option.ToString() + " : " + _buffOptions[(int)option]);
    }

    public void ForceAddBuff(float value, Ability.Buff option)
    {
        if (!CheckBuffOvelapped(option))
            return;
        _buffOptions[(int)option] += value;
        _buffOptionsOvelapped[(int)option] += 1;
        Debug.Log("add buff : " + option.ToString() + " : " + _buffOptions[(int)option]);
    }

    public void ForceOffBuff(float value, Ability.Buff option)
    {
        if (!CheckBuffOvelapped(option))
            return;
        _buffOptions[(int)option] -= value;
        _buffOptionsOvelapped[(int)option] -= 1;
        Debug.Log("add buff : " + option.ToString() + " : " + _buffOptions[(int)option]);
    }

    bool CheckBuffOvelapped(Ability.Buff option)
    {
        bool result = true;
        switch (option)
        {
            case Ability.Buff.MoveSpeed_Buff:
            case Ability.Buff.AttackSpeed_Buff:
                if (_buffOptionsOvelapped[(int)option] >= 4)
                    result = false;
                break;
            case Ability.Buff.Attack_Buff:
            case Ability.Buff.Defense_Buff:
            case Ability.Buff.Accuracy_Buff:
            case Ability.Buff.CriticalChance_Buff:
                if (_buffOptionsOvelapped[(int)option] >= 1)
                    result = false;
                break;
            case Ability.Buff.EnumTotal:
                break;
            default:
                break;
        }
        return result;
    }
}
