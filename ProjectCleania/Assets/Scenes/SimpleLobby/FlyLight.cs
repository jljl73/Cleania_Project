using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyLight : MonoBehaviour
{
    Light _light;
    public float moveScale = 1.0f;
    public float lightPow = 1.0f;
    float rand_x;
    float rand_y;
    float timecount;
    float rand;

    // Start is called before the first frame update
    void Start()
    {
        _light = GetComponent<Light>();
        rand = Random.Range(0.0f, 2.0f);
        timecount = rand;
        rand_x = Random.Range(-moveScale, moveScale);
        rand_y = Random.Range(-moveScale, moveScale);
    }

    // Update is called once per frame
    void Update()
    {
        if (timecount > 2)
        {
            rand_x = Random.Range(-moveScale, moveScale);
            rand_y = Random.Range(-moveScale, moveScale);
            timecount = Random.Range(0.0f, 1.0f);
        }

        timecount += Time.deltaTime;

        Vector3 loc = transform.right * (rand_x + Mathf.Sin(Time.time/2 + rand)/2) + transform.up * (rand_y + Mathf.Cos(Time.time/2 + rand)/2);
        transform.Translate(loc * Time.deltaTime);
        _light.range = lightPow;
    }
}
