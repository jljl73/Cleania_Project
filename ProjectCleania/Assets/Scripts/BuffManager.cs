using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    public enum Option
    {
        MoveSpeed, AttackSpeed, Attack, Defense
    }

    float[] _options = { 0, 0, 0, 0 };

    public float this[int index]
    {
        get => _options[index];
    }

    public void AddBuff(float value, Option option, float duration)
    {
        _options[(int)option] += value;
        StartCoroutine(OffBuff(value, option, duration));
    }

    IEnumerator OffBuff(float value, Option option, float duration)
    {
        yield return new WaitForSeconds(duration);
        _options[(int)option] -= value;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(_options[(int)Option.MoveSpeed]);
        }
    }

}
