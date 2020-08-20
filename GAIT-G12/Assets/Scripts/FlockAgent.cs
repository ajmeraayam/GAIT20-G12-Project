using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    Vector3 currentVelocity;
    public Vector3 CurrentVelocity { get { return currentVelocity; } }
    float maxVelocity = 10f;
    public float MaxVelocity { get { return maxVelocity; } }
    //Use tags
    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } }
    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider>();
        transform.gameObject.tag = "Human";
    }

    public void Move(Vector3 velocity)
    {
        currentVelocity = velocity;
        transform.up = velocity;
        transform.position += velocity * Time.deltaTime;
    }
}
