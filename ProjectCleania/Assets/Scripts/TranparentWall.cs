using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranparentWall : MonoBehaviour
{
    private Renderer rend;
    private Color materialColor;

    private bool tranparent = false;

    //void Awake()
    //{
    //    rend = GetComponent<Renderer>();
    //    materialColor = rend.material.color;
    //    //materialColor = rend.material.HasProperty("_Color");
    //}

    //public void MakeTransparent(bool _value)
    //{
    //    // 이미 바뀌어 있으면 리턴
    //    if (_value == tranparent) return;

    //    this.tranparent = _value;

    //    if (_value)
    //    {
    //        // 투명화
    //        //if (rend.material.HasProperty("_Color"))
    //        //{
    //        //    rend.material.SetColor("_Color", Color.red);
    //        //}
    //        materialColor.a = 0.3f;
    //    }
    //    else
    //    {
    //        // 불투명화
    //        materialColor.a = 1f;
    //    }

    //    // 새로운 칼라 할당
    //    rend.material.color = materialColor;
    //}
}
