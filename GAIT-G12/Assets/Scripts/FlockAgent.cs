﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class FlockAgent : MonoBehaviour
{
    Vector2 currentVelocity;
    public Vector2 CurrentVelocity { get { return currentVelocity; } }
    float maxVelocity = 1f;
    public float MaxVelocity { get { return maxVelocity; } set { maxVelocity = value; } }
    //Use tags
    Collider2D agentCollider;
    public Collider2D AgentCollider { get { return agentCollider; } }

    Flock agentFlock;
    public Flock AgentFlock { get { return agentFlock; } }

    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<Collider2D>();
        //Use spawner script to provide tags
        //transform.gameObject.tag = "Human";
    }

    public void Initialize(Flock flock)
    {
        agentFlock = flock;
    }
    public void Move(Vector2 velocity)
    {
        currentVelocity = velocity;
        transform.up = velocity;
        transform.position += (Vector3) velocity * Time.deltaTime;
    }
}
