using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGroupManager : MonoBehaviour
{
    List<GameObject> enemies = new List<GameObject>();
    GameObject target;
    public GameObject Target
    {
        get { return target; }
        set
        {
            foreach (var e in enemies)
            {
                e.GetComponent<Enemy>().SetTarget(value);
            }
            this.target = value;
        }
    }
    const float chasingDistance = 10.0f;
    // const float chasingDistance = 5.0f;
    int sum = 0;

    private void OnDisable()
    {
        target = null;
    }

    void Update()
    {
        if (Target?.GetComponent<AbilityStatus>().HP == 0)
        {
            sum = 1;
            ReleaseTarget();
        }
    }

    public void AddMember(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void DeleteMember(GameObject enemy)
    {
        enemies.Remove(enemy);
        if (enemies.Count == 0)
            Target = null;
    }

    public bool IsMemberExist(GameObject enemy)
    {
        if (enemies.Contains(enemy))
            return true;
        else
            return false;
    }

    public bool IsEmpty()
    {
        if (enemies.Count == 0)
            return true;
        else
            return false;
    }

    public int Count()
    {
        return enemies.Count;
    }

    //public void SetTarget(GameObject target)
    //{
    //    foreach (var e in enemies)
    //    {
    //        e.GetComponent<Enemy>().SetTarget(target);
    //    }
    //    this.target = target;
    //}

    public void SetTarget()
    {
        ++sum;
        //foreach (var e in enemies)
        //{
        //    e.GetComponent<Enemy>().SetTarget(target);
        //}
        //this.target = target;
    }

    public void ReleaseTarget()
    {
        if (--sum > 0) return;
        //print("a");
        //if (CheckCollidedObject() > 0) return;

        //print("b");
        foreach (var e in enemies)
            e.GetComponent<Enemy>().ReleaseTarget();
    }

    int CheckCollidedObject()
    {
        int sum = 0;
        if (target == null) return 1;

        foreach (var e in enemies)
        {
            print("Target: " + target.name);
            print("Vector3.Distance: " + Vector3.Distance(target.transform.position, e.transform.position));
            if (Vector3.Distance(target.transform.position, e.transform.position) < chasingDistance)
                ++sum;
        }

        print("sum: " + sum);

        return sum;
    }
}
