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
    //    // �̹� �ٲ�� ������ ����
    //    if (_value == tranparent) return;

    //    this.tranparent = _value;

    //    if (_value)
    //    {
    //        // ����ȭ
    //        //if (rend.material.HasProperty("_Color"))
    //        //{
    //        //    rend.material.SetColor("_Color", Color.red);
    //        //}
    //        materialColor.a = 0.3f;
    //    }
    //    else
    //    {
    //        // ������ȭ
    //        materialColor.a = 1f;
    //    }

    //    // ���ο� Į�� �Ҵ�
    //    rend.material.color = materialColor;
    //}
}
