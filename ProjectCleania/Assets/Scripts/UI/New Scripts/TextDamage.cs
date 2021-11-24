using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDamage : MonoBehaviour
{
    public float speed;
    Color color;
    float a;

    private void Start()
    {
        Destroy(gameObject, 2.0f);
        color = GetComponent<TextMesh>().color;
        a = color.a;
    }
    public void SetDamageText(string value)
    {
        GetComponent<TextMesh>().text = value;
    }

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        a -= Time.deltaTime * 0.5f;
        GetComponent<TextMesh>().color = new Color(color.r, color.g, color.b, a);
    }
}
