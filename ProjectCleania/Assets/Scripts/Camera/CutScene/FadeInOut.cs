using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeInOut : MonoBehaviour
{
    Sprite sprite;
    WaitForSeconds wait = new WaitForSeconds(0.1f);
    float alpha = 0;

    private void Start()
    {
        sprite = GetComponent<Image>().sprite;
    }

    
    public void FadeOut()
    {
        StartCoroutine(IFadeOut());
    }

    IEnumerator IFadeOut()
    {
        Color color = GetComponent<Image>().color;
        alpha = 0.0f;
        while (alpha < 1.0f)
        {
            GetComponent<Image>().color = new Color(color.r, color.g, color.b, alpha);
            alpha += 0.15f;
            yield return wait;
        }
        GetComponent<Image>().color = new Color(color.r, color.g, color.b, 1.0f);

        yield return new WaitForSeconds(1.0f);
        StartCoroutine(IFadeIn());
    }

    public void FadeIn()
    {
        StartCoroutine(IFadeIn());
    }

    IEnumerator IFadeIn()
    {
        Color color = GetComponent<Image>().color;
        alpha = 1.0f;
        while (alpha > 0)
        {
            GetComponent<Image>().color = new Color(color.r, color.g, color.b, alpha);
            alpha -= 0.15f;
            yield return wait;
        }
        GetComponent<Image>().color = new Color(color.r, color.g, color.b, 0.0f);
    }
}

