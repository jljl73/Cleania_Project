using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitTest : MonoBehaviour
{
    public List<GameObject> winds = new List<GameObject>();
    public float rotateSpeed = 30;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MakeWindRotateAroundMe();
    }

    void MakeWindRotateAroundMe()
    {
        for (int i = 0; i < winds.Count; i++)
        {
            winds[i].transform.RotateAround(this.transform.position, this.transform.up, rotateSpeed * Time.deltaTime);
        }
    }
}
