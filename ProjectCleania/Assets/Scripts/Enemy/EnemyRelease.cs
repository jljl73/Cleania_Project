using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRelease : MonoBehaviour
{
    public GameObject enemySpawner;

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            enemySpawner.GetComponent<EnemyGroupManager>().ReleaseTarget();
    }
}
