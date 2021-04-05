using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TrailRenderer))]
public class Thruster : MonoBehaviour
{
    TrailRenderer Trail;
    private void Awake()
    {
        Trail = GetComponent<TrailRenderer>();
    }
    public void ActivateThrusters(bool activate = true)
    {
        if (activate)
        {
            Trail.enabled = true;
        }
        else 
        { 
           
            Trail.enabled = false; 
            Trail.Clear();
        }

    }
}
