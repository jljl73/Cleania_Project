using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuffManager : MonoBehaviour
{
    float[] _options = { 0, 0, 0, 0 };

    public float this[AbilityOption.Name index]
    {
        get => _options[(int)index];
    }

    public void AddBuff(float value, AbilityOption.Name option, float duration)
    {
        _options[(int)option] += value;
        StartCoroutine(OffBuff(value, option, duration));
    }

    IEnumerator OffBuff(float value, AbilityOption.Name option, float duration)
    {
        yield return new WaitForSeconds(duration);
        _options[(int)option] -= value;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            Debug.Log(_options[(int)AbilityOption.Name.MoveSpeed_Buff]);
        }
    }

}
