using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSpin : MonoBehaviour
{
    public float SpeenSpeed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        ComeBackLobby.Init();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, SpeenSpeed * Time.deltaTime, 0, Space.World);
    }
}
