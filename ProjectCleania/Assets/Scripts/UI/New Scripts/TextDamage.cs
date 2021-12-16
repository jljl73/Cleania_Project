using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using TMPro;

public class TextDamage : MonoBehaviour
{
    public float FadeOutTime = 2.0f;
    public float speed;
    Color color;
    float a;
    StringBuilder sb = new StringBuilder();

    private void Start()
    {
        Destroy(gameObject, FadeOutTime);
        color = GetComponent<TextMeshPro>().color;
        a = color.a;
    }
    public void SetDamageText(Ability.AffectResult result)
    {
        sb.Clear();

        if(result.Enemy)
        {
            GetComponent<TextMeshPro>().fontSize = 6f;
            sb.Append("<color=#FF0000>");
            sb.Append(string.Format("{0:N0}", result.Value));
            sb.Append("</color>");
        }
        else if (result.Dodged)
        {
            GetComponent<TextMeshPro>().fontSize = 8f;
            sb.Append("<color=#D0CECE>");
            sb.Append("ºø³ª°¨!");
            sb.Append("</color>");
        }
        else if(result.Heal)
        {
            GetComponent<TextMeshPro>().fontSize = 8f;
            sb.Append("<color=#00FF00>");
            sb.Append(string.Format("{0:N0}", result.Value));
            sb.Append("</color>");
        }
        else if (result.Critical && UserSetting.OnCriticalDamage)
        {
            GetComponent<TextMeshPro>().fontSize = 10f;
            sb.Append("<color=#FAEE4C>");
            sb.Append(string.Format("{0:N0}", result.Value));
            sb.Append("</color>");
        }
        else
        {
            GetComponent<TextMeshPro>().fontSize = 8f;
            sb.Append(string.Format("{0:N0}", result.Value));
        }

        GetComponent<TextMeshPro>().text = sb.ToString();
    }

    void Update()
    {
        transform.rotation = Camera.main.transform.rotation;
        transform.Translate(Vector3.up * speed * Time.deltaTime);
        a -= Time.deltaTime / FadeOutTime;
        GetComponent<TextMeshPro>().color = new Color(color.r, color.g, color.b, a);
    }
}
