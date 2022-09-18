using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyBehaviour : EntityBehaviour
{
    public override void InitParams()
    {   
        speedfactor = 0.2f;
        timefactor = 0f;
        fovDistance = 20;
        fovRange = 240;
        fovRays = 30;
        visibleLayers = LayerMask.GetMask("Predator");
    }

    private void Update()
    {
        energy += Mathf.Lerp(10, -5, Mathf.Abs(speed) / MaxSpeed) * Time.deltaTime;
        lifeTime = lifeTime * Time.deltaTime;
        if (energy <= 0)
        {
            MaxSpeed = 0;
        }
        else 
        {
            MaxSpeed = 7;
        }
    }


    public override void HandleCollisionWithEntitity(EntityBehaviour entityBehaviour)
    {
        if (entityBehaviour is PredatorBehaviour)
        {
            Destroy(gameObject);
        }
    }
}
