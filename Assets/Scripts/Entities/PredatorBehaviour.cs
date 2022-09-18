using System.Collections;
using System.Collections.Generic;
using UnityEngine; 

public class PredatorBehaviour : EntityBehaviour
{
    public override void InitParams()
    {
        eatCounter = 0;
        timefactor = 1f;
        MaxSpeed = 6;
        fovDistance = 30;
        fovRange = 20;
        fovRays = 10;
        visibleLayers = LayerMask.GetMask("Prey");
    }

public void Update()
{
    if (energy <= 0)
        {
            Destroy(gameObject);
        }
}

public override void HandleCollisionWithEntitity(EntityBehaviour entityBehaviour)
    {
        if (entityBehaviour is PreyBehaviour)
        {
            energy += 30;
            eatCounter++;
        }
    }

}
