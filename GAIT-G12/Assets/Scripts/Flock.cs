using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviour behaviour;
    [Range(1f, 100f)] public float driveFactor = 10f;
    float maxSpeed;
    [Range(1f, 10f)] public float neighbourRadius = 2f;
    [Range(0f, 5f)] public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    
    void Start()
    {
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
    }

    void Update()
    {
        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            Vector2 move;

            move = behaviour.calculateMove(agent, context, this);
            move *= driveFactor;
            
            maxSpeed = agent.MaxVelocity;
            squareMaxSpeed = maxSpeed * maxSpeed;

            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadius);
        foreach(Collider2D col in contextColliders)
        {
            if(col != agent.AgentCollider)
            {
                context.Add(col.transform);
            }
        }

        return context;
    }

    public void AddAgent(FlockAgent agent)
    {
        agents.Add(agent);
    }

    public void RemoveAgent(FlockAgent agent)
    {
        if(agents.Contains(agent))
        {
            bool cond = agents.Remove(agent);
            if(cond)
            {
                Destroy(agent.gameObject);
            }
        }
    }

    public void PrintMessage(string message)
    {
        print(message);
    }
}
