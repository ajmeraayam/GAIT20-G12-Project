using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Can only be attached to GameObjects that have a 2d collider on them
[RequireComponent(typeof(Collider2D))]
/*
 * The class which represents a zombie as a flock agent
 */
public class FlockAgent : MonoBehaviour
{
    // Current velocity of the agent 
    Vector2 currentVelocity;
    public Vector2 CurrentVelocity { get { return currentVelocity; } }
    // Maximum velocity of this agent. Keeps changing according to the state of this agent
    float maxVelocity = 1f;
    public float MaxVelocity { get { return maxVelocity; } set { maxVelocity = value; } }
    // Collider attached to the GameObject this script is on
    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }
    // Flock object this agent belongs to
    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
    }

    // Initialize the agent with the Flock object
    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }

    /*
     * Moves the agent in given direction. The input comes from the Flock class 
     * after all the behaviours are calculated.
     */
    public void Move(Vector2 velocity)
    {
        currentVelocity = velocity;
        transform.up = velocity;
        transform.position += (Vector3) velocity * Time.deltaTime;
    }

    // Destroy this agent by removing it from the flock
    public void DestroyAgent()
    {
        agentFlock.RemoveAgent(this);
    }
}
