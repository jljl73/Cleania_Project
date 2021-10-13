using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Testpp : MonoBehaviour
{
    public PlayerSkillFairysWingsSO skilCO;


    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            string tempString = skilCO.GetSkillDetails();
            print(skilCO.GetSkillDetails());
        }
    }
}
