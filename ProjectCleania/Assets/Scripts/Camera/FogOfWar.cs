using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FogOfWar : MonoBehaviour
{
    public Material projectorMaterial;
    public float blendSpeed;
    public int textureScale;

    public RenderTexture fogTexture;
    RenderTexture prevTexture;
    RenderTexture curTexture;
    Projector projector;

    float blendAmount;

    void Awake()
    {
        projector = GetComponent<Projector>();
        projector.enabled = true;

        prevTexture = GenerateTexture();
        curTexture = GenerateTexture();

        projector.material = new Material(projectorMaterial);

        projector.material.SetTexture("_PrevTexture", prevTexture);
        projector.material.SetTexture("_CurTexture", curTexture);

        StartNewBlend();

    }

    RenderTexture GenerateTexture()
    {
        RenderTexture rt = new RenderTexture(
            fogTexture.width * textureScale,
            fogTexture.height * textureScale,
            0, fogTexture.format)
        { filterMode = FilterMode.Bilinear };
        rt.antiAliasing = fogTexture.antiAliasing;

        return rt;
    }

    public void StartNewBlend()
    {
        StopCoroutine(BlendFog());
        blendAmount = 0;

        Graphics.Blit(curTexture, prevTexture);
        Graphics.Blit(fogTexture, curTexture);

        StartCoroutine(BlendFog());
    }

    IEnumerator BlendFog()
    {
        while(blendAmount < 1)
        {
            blendAmount += Time.deltaTime * blendSpeed;
            projector.material.SetFloat("_Blend", blendAmount);
            yield return null;
        }
        StartNewBlend();
    }
}
