using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StormWindController : DamagingProperty
{
    [SerializeField]
    GameObject stormWindPrefab;

    //List<GameObject> winds = new List<GameObject>();

    bool rotateCW = true;
    float rotateRadius = 3f;
    float rotateSpeed = 90f;
    int count;
    float duration;

    // Initiate 후 함수 호출 필수
    public void SetUp(bool isCW, float rotateRadius, float rotateSpeed, int count, float duration, AbilityStatus abil, float damageScale)
    {
        this.rotateCW = isCW;
        this.rotateRadius = rotateRadius;
        this.rotateSpeed = rotateSpeed;
        this.count = count;
        this.duration = duration;

        base.SetUp(abil, damageScale);

        MakeWinds(isCW);
        Destroy(this.gameObject, duration);
    }

    void MakeWinds(bool isCW)
    {
        for (int i = 0; i < count; i++)
        {
            GameObject obj = Instantiate(stormWindPrefab, GetRandomPosOnCircleEdge(), transform.rotation, transform);
            // winds.Add(obj);

            StormWind stormWind = obj.GetComponent<StormWind>();
            if (stormWind != null)
            {
                if (isCW)
                    stormWind.SetUp(this.gameObject, rotateSpeed);
                else
                    stormWind.SetUp(this.gameObject, -rotateSpeed);

                stormWind.SetUp(ownerAbility, damageScale);
            }

        }
    }

    Vector3 GetRandomPosOnCircleEdge()
    {
        float rad = Random.Range(0f, 2 * Mathf.PI);
        if (!rotateCW)
            rad *= -1;

        return transform.position + new Vector3(rotateRadius * Mathf.Sin(rad), 0, rotateRadius * Mathf.Cos(rad));
    }
}
