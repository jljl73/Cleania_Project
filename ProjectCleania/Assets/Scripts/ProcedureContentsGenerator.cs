using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

public class ProcedureContentsGenerator : MonoBehaviour
{
    public enum CategoriesCountType { Three, Five, Ten };

    public float MapWeight = 1000;
    public int RareMonsterCount = 6;
    public CategoriesCountType CurrentCategoriesCount = CategoriesCountType.Ten;
    public List<GameObject> CategoriesList;
    List<EnemySpawner> enemySpawners;

    float weightPerCategories;

    void Start()
    {
        print("1");
        foreach (GameObject Categories in CategoriesList)
        {
            Categories.SetActive(false);
        }
        print("2");
        CategoriesList[(int)CurrentCategoriesCount].SetActive(true);
        print("3");
        enemySpawners = new List<EnemySpawner>();
        enemySpawners.AddRange(CategoriesList[(int)CurrentCategoriesCount].GetComponentsInChildren<EnemySpawner>());
        print("enemySpawners.count: " + enemySpawners.Count);
        SetWeightPerCategories();
        SetRareMosterCount();
        SpawnStart();
    }

    void SetWeightPerCategories()
    {
        switch (CurrentCategoriesCount)
        {
            case CategoriesCountType.Three:
                weightPerCategories = MapWeight / 3f;
                break;
            case CategoriesCountType.Five:
                weightPerCategories = MapWeight / 5f;
                break;
            case CategoriesCountType.Ten:
                weightPerCategories = MapWeight / 10f;
                break;
            default:
                break;
        }

        for (int i = 0; i < enemySpawners.Count; i++)
        {
            enemySpawners[i].Weight = weightPerCategories;
        }
    }

    void SetRareMosterCount()
    {
        // ¼¯±â
        enemySpawners = enemySpawners.OrderBy(i => Guid.NewGuid()).ToList();
        int tempRareMonsterCount = RareMonsterCount;

        for (int i = 0; i < enemySpawners.Count; i++)
        {
            if (tempRareMonsterCount == 0)
                return;

            enemySpawners[i].RareMonsterCount = 1;
            tempRareMonsterCount--;
        }
    }

    void SpawnStart()
    {
        for (int i = 0; i < enemySpawners.Count; i++)
        {
            enemySpawners[i].Spawn();
        }
    }

}
