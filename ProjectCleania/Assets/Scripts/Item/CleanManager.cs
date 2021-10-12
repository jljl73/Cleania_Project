using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CleanManager : MonoBehaviour
{
    public int clean = 0;
    public Text text;

    void Start()
    {
        text.text = clean.ToString();
    }

    void AcquireClean(int value)
    {
        clean += value;
    }

    bool UseClean(int value)
    {
        if (clean < value) return false;

        clean -= value;
        return true;
    }
}
