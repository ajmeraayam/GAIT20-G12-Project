using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanSeek : MonoBehaviour
{

    Rigidbody2D rigidBody;
    float mass = 15;
    [SerializeField] float MaxVelocity = 3f;
    [Tooltip("Maximum steering force that can be applied to object")] [SerializeField] float MaxForce = 15f;
    float speed = 3;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = gameObject.GetComponent<Rigidbody2D>();
    }

    public void seekPosition(Vector3 target)
    {
        Vector3 newPosition = transform.position + (Vector3)rigidBody.velocity;
        Vector3 currentVelocity = (newPosition - transform.position).normalized * MaxVelocity;
        Vector3 desiredVelocity = (target - transform.position).normalized * MaxVelocity;
        Vector3 steering = desiredVelocity - currentVelocity;
        steering = Vector3.ClampMagnitude(steering, MaxForce);
        steering = steering / mass;
        Vector3 velocity = Vector3.ClampMagnitude(currentVelocity + steering, speed);
        transform.position = transform.position + velocity;
    }
}
