using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    float[] _options = { 1, 1, 1, 1 };

    public float this[Ability.Buff index]
    {
        get => _options[(int)index];
    }

    public void AddBuff(float value, Ability.Buff option, float duration)
    {
        _options[(int)option] += value;
        StartCoroutine(OffBuff(value, option, duration));
        Debug.Log("add buff : " + option.ToString() + " : " + _options[(int)option]);
    }

    IEnumerator OffBuff(float value, Ability.Buff option, float duration)
    {
        yield return new WaitForSeconds(duration);
        _options[(int)option] -= value;
        Debug.Log("off buff : " + option.ToString() + " : " + _options[(int)option]);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(_options[(int)Ability.Buff.Attack_Buff]);
        }
    }

}
