using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraType : MonoBehaviour
{


    [SerializeField] Camera[] Cameras;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            for(int i = 0; i < Cameras.Length; i++)
            {
                if (Cameras[i].enabled)
                {
                    Cameras[i].enabled = false;
                    if (i == Cameras.Length - 1)
                        Cameras[0].enabled = true;
                    else Cameras[i + 1].enabled = true;
                    break;
                }
            }
        }
    }
}
