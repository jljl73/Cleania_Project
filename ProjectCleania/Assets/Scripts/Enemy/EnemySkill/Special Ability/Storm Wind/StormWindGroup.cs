using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StormWindGroup
{
    Vector3 centerPos;
    Quaternion centerQuaternion;

    public bool RotateCW = true;
    public float RotateRadius = 3f;
    public float RotateSpeed = 90f;

    GameObject stormWindPrefab;
    List<GameObject> stormWinds = new List<GameObject>();

    public StormWindGroup(GameObject stormWindPrefab, Vector3 centerPos, Quaternion centerQuaternion, bool rotateCW, float rotateRadius, float rotateSpeed, int count)
    {
        this.centerPos = centerPos;
        this.centerQuaternion = centerQuaternion;
        this.RotateCW = rotateCW;
        this.RotateRadius = rotateRadius;
        this.RotateSpeed = rotateSpeed;

        for (int i = 0; i < count; i++)
        {
            GameObject obj = GameObject.Instantiate(stormWindPrefab, GetRandomPosOnCircleEdge(), centerQuaternion);
            stormWinds.Add(obj);

            StormWind stormWind = obj.GetComponent<StormWind>();
            if (stormWind != null)
            {
                //if (i % 2 == 0)
                //    stormWind.SetUp(centerPos , - RotateSpeed);
                //else
                //    stormWind.SetUp(centerPos, RotateSpeed);
            }
        }
    }

    Vector3 GetRandomPosOnCircleEdge()
    {
        float rad = Random.Range(0f, 2 * Mathf.PI);
        if (!RotateCW)
            rad *= -1;

        return centerPos + new Vector3(RotateRadius * Mathf.Sin(rad), 0, RotateRadius * Mathf.Sin(rad));
    }
}
