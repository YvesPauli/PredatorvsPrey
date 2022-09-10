using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PredatorBehaviour : EntityBehaviour
{
    private void Awake()
    {
        fovDistance = 30;
        fovRange = 20;
        fovRays = 10;
        visibleLayers = LayerMask.GetMask("Prey");
    }

    private void Update()
    {
        if (energy >= 120)
        {
            energy = 60;
            Instantiate(gameObject);
        }
    }
}
