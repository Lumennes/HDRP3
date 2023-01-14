using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {       
      

        if (Input.GetKeyDown(KeyCode.Alpha1))
            QualitySettings.SetQualityLevel(0);
        if (Input.GetKeyDown(KeyCode.Alpha2))
            QualitySettings.SetQualityLevel(1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            QualitySettings.SetQualityLevel(2);
    }

}
