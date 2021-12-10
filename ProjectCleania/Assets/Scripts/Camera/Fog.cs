using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fog : MonoBehaviour
{
    public GameObject m_fogOfWarPlane;
    public Transform m_player;
    public LayerMask m_fogLayer;
    public Material material;
    public float m_radius = 5f;
    float m_radiusSqr {  get { return m_radius * m_radius; } }

    Mesh m_mesh;
    MeshCollider collider;
    Vector3[] m_vertices;
    Color[] m_colors;

    //float width = 400.0f;
    //float height = 400.0f;
    WaitForSeconds wait = new WaitForSeconds(0.5f);

    void Start()
    {
        //CreateMesh();
        Initialize();
        if (m_player == null)
            Debug.Log("Player is null");

        StartCoroutine(Clear());
    }

    // Update is called once per frame
    IEnumerator Clear()
    {
        while (true)
        {
            Vector3 orthogonal = m_player.position;
            orthogonal.y = 100;
            Ray r = new Ray(orthogonal, Vector3.down);
            RaycastHit hit;
            if (Physics.Raycast(r, out hit, 100, m_fogLayer, QueryTriggerInteraction.Collide))
            {
                for (int i = 0; i < m_vertices.Length; ++i)
                {
                    Vector3 v = m_fogOfWarPlane.transform.TransformPoint(m_vertices[i]);
                    float dist = Vector3.SqrMagnitude(v - hit.point);
                    if (dist < m_radiusSqr)
                    {
                        float alpha = Mathf.Min(m_colors[i].a, dist / m_radiusSqr);
                        m_colors[i].a = alpha;

                    }
                }
                UpdateColor();
            }
            yield return wait;
        }
    }

    void Initialize()
    {
        m_mesh = m_fogOfWarPlane.GetComponent<MeshFilter>().mesh;

        m_vertices = m_mesh.vertices;
        m_colors = new Color[m_vertices.Length];
        for (int i = 0; i < m_colors.Length; ++i)
        {
            m_colors[i] = Color.black;
            Debug.Log(m_vertices[i]);
        }
        UpdateColor();
    }

    void UpdateColor()
    {
        m_mesh.colors = m_colors;
    }

}



