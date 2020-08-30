using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : MonoBehaviour
{
    Rigidbody rigidBody;
    float mass;
    [SerializeField] float MaxVelocity = 3f;
    [Tooltip("Maximum steering force that can be applied to object")][SerializeField] float MaxForce = 15f;

    private Vector3 velocity;
    [SerializeField] Rigidbody target;
    private bool collided = false;
    // Start is called before the first frame update
    void Start()
    {
        velocity = Vector3.zero;
        rigidBody = GetComponent<Rigidbody>();
        mass = rigidBody.mass;
    }

    // Update is called once per frame
    void Update()
    {
        if(!collided)
        {
            var desiredVelocity = Vector3.Normalize(target.transform.position - transform.position) * MaxVelocity;
            var steering = desiredVelocity - velocity;

            steering = Vector3.ClampMagnitude(steering, MaxForce);
            steering /= mass;

            velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
            transform.position += velocity * Time.deltaTime;
            transform.forward = velocity.normalized;
        }        
        else
        {
            velocity = Vector3.zero;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        collided = true;
    }

    void OnCollisionExit(Collision collision)
    {
        collided = false;
    }
}
