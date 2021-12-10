using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.IO;

public class ExpManager
{
    static List<int> needExp = new List<int>();
    static public int Exp { get; private set; }

    static public void Initailize(int Exp)
    {
        string path = $"{Application.dataPath}/savedata/ExpTable.txt";
        StreamReader reader = new StreamReader(path);

        while (!reader.EndOfStream)
        {
            needExp.Add(int.Parse(reader.ReadLine()));
        }
        reader.Close();

        ExpManager.Exp = Exp;
    }

    static public void Acquire(int nExp)
    {
        ExpManager.Exp += nExp;
        GameManager.Instance.chatManager.ShowAcquireExp(nExp);
    }

    static public int Level
    {
        get
        {
            int i = 0;
            for(i = 0; i < needExp.Count;++i)
            {
                if (Exp < needExp[i])
                    break;
            }
            return i;
        }
    }

    static public float Percent
    {
        get
        {
            int level = Level;
            if (level >= needExp.Count) return 0f;

            return (float)(Exp - needExp[level-1]) / (float)(needExp[level] - needExp[level-1]);
        }
    }
}

