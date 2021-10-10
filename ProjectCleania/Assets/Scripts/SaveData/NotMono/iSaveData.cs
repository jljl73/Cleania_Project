using UnityEngine;

interface iSaveData
{
    string ToJson();
    void FromJson(string Json);

}

