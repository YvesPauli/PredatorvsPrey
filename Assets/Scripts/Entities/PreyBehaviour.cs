using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PreyBehaviour : EntityBehaviour
{
    private void Awake()
    {
        fovDistance = 20;
        fovRange = 60;
        fovRays = 10;
        visibleLayers = LayerMask.GetMask("Predator");
    }

    private void Update()
    {
        energy += Mathf.Lerp(10, 0, Mathf.Abs(speed) / MaxSpeed) * Time.deltaTime;
    }

    public override void HandleCollisionWithEntitity(EntityBehaviour entityBehaviour)
    {
        if (entityBehaviour is PredatorBehaviour)
        {
            Destroy(gameObject);
        }
    }
}
