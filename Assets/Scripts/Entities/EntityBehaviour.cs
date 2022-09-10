using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityBehaviour : MonoBehaviour
{
    public const float MaxSpeed = 5;
    public const float MaxAngularSpeed = 100;

    public float fovDistance; // in meters
    public float fovRange; // in degrees
    public int fovRays;

    public LayerMask visibleLayers;

    public float speed = 0;
    public float omega = 0;

    public float energy = 100;

    private Brain brain;

    // Start is called before the first frame update
    void Start()
    {
        brain = new Brain(fovRays, new int[] { 5,5 }, 2);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        var FoV = CalculateFoV();
        var AIoutput = brain.Process(FoV);
        speed = Mathf.Abs(AIoutput[0]* MaxSpeed);
        omega = AIoutput[1]* MaxAngularSpeed;
        transform.position += transform.right * Time.deltaTime * speed;
        GetComponent<Rigidbody2D>().angularVelocity = omega;

        energy = energy - Time.deltaTime * Mathf.Abs(speed) / MaxSpeed;

        if (energy <= 0)
        {
            Destroy(gameObject);
        }

        if (energy >= 120)
        {
            energy = 60;
            Instantiate(gameObject);
        }

    }

    private float[] CalculateFoV()
    {
        Debug.Assert(fovRays != 1);


        float deltaAngle = fovRange / (fovRays - 1);
        float[] distances = new float[fovRays];
        for (int i = 0; i < fovRays; i++)
        {
            float angle = -fovRange/ 2f + i * deltaAngle;
            var dir = new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad));

            dir = transform.TransformDirection(dir);

            var hit = Physics2D.Raycast(transform.position, dir, fovDistance, visibleLayers);

            var distance = fovDistance;
            var isHit = false;

            if (hit.collider != null)
            {
                distance = hit.distance;
                isHit = true;
            }

            Debug.DrawRay(transform.position, dir * distance, isHit ? Color.red : Color.white);

            distances[i] = distance / fovDistance;
        }

        return distances;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<EntityBehaviour>(out var entityBehaviour))
        {
            HandleCollisionWithEntitity(entityBehaviour);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.TryGetComponent<Border>(out var border))
        {
            var pos = transform.position;
            switch(border.borderType)
            {
                case Border.BorderType.LeftRight:
                    pos.x = -pos.x;
                    break;
                case Border.BorderType.TopBot:
                    pos.y = -pos.y;
                    break;
            }
            transform.position = pos;
/*            var offset = border.transform.position - transform.position;
            transform.position = border.oppositeBorder.transform.position - offset;*/
            // border.oppositeBorder
        }
    }

    public virtual void HandleCollisionWithEntitity(EntityBehaviour entityBehaviour)
    {

    }
}
