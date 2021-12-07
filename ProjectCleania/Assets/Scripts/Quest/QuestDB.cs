using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;


public class QuestDB : MonoBehaviour
{

    [System.Serializable]
    public class SerializableData<T>
    {
        [SerializeField]
        public List<T> datas= new List<T>();  
        public List<T> ToList() {  return datas; }

        public void Add(T data)
        { this.datas.Add(data); }

        public void Load(List<T> datas)
        {
            for(int i = 0; i < datas.Count; ++i)
                this.datas.Add(datas[i]);
        }

        public void Clear()
        {
            this.datas.Clear();
        }
    }
    static QuestDB _instance;

    public static QuestDB Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject newGameObject = new GameObject("QuestDB");
                _instance = newGameObject.AddComponent<QuestDB>();
                DontDestroyOnLoad(newGameObject);
            }
            return _instance;
        }
    }

    [System.Serializable]
    public struct QuestData
    {
        public int ID;
        public int state;
        public string progress;
        public QuestData(Quest quest)
        {
            this.ID = quest.ID;
            this.state = (int)quest.State;
            this.progress = quest.Incoding();
        }
    }

    [SerializeField]
    SerializableData<QuestData> questsDatas = new SerializableData<QuestData>();
    string characterName;

    public void SetNickName(string name)
    {
        characterName = name;
    }

    public void Save(List<Quest> quests)
    {
        questsDatas.Clear();
        for (int i = 0; i < quests.Count; ++i)
            questsDatas.Add(new QuestData(quests[i]));

        string json = JsonUtility.ToJson(questsDatas, true);
        File.WriteAllText($"{Application.dataPath}/savedata/Quest_{characterName}.json", json);
    }

    public void Load(List<Quest> quests)
    {
        questsDatas.Clear();
        string path = $"{Application.dataPath}/savedata/Quest_{characterName}.json";
        if (!File.Exists(path))
        {
            for (int i = 0; i < quests.Count; ++i)
                quests[i].Reset();
            return;
        }

        string json = File.ReadAllText(path);
        List<QuestData> datas = JsonUtility.FromJson<SerializableData<QuestData>>(json).ToList();

        for (int i = 0; i < datas.Count; ++i)
        {
            quests[i].Load((Quest.STATE)datas[i].state, datas[i].progress);
        }
    }

    private void Update()
    {
        
    }
}
