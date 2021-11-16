using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class QuestDB : MonoBehaviour
{
    static string characterName;
    static List<Quest> quests;

    public static void SetNickName(string name)
    {
        characterName = name;
    }

    public static void Save(List<Quest> quests)
    {
        string json = JsonUtility.ToJson(quests);
        File.WriteAllText($"{Application.dataPath}/DB/{characterName}Quest.json", json);
    }

    public static List<Quest> Load()
    {
        string path = $"{Application.dataPath}/DB/{characterName}Quest.json";
        string json = File.ReadAllText(path);
        quests = JsonUtility.FromJson<List<Quest>>(json);

        Debug.Log(quests);
        return quests;
    }

    private void Update()
    {
        
    }
}
