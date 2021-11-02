using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormWind : MonoBehaviour
{
    GameObject rotatePivot;
    public float rotateSpeed;


    private void Update()
    {
        MakeWindRotateAroundMe();
    }

    public void SetUp(GameObject rotatePivot, float rotateSpeed)
    {
        this.rotatePivot = rotatePivot;
        this.rotateSpeed = rotateSpeed;
    }

    void MakeWindRotateAroundMe()
    {
        if (rotatePivot == null) return;
        print("rotateSpeed * Time.deltaTime: " + rotateSpeed * Time.deltaTime);
        transform.RotateAround(rotatePivot.transform.position, rotatePivot.transform.up, rotateSpeed * Time.deltaTime);
    }
}
