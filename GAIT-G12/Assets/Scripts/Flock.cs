using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * The script that keeps record of all the agents
 * and manages all the agents that are associated with this flock
 */
public class Flock : MonoBehaviour
{
    // List of agents that are associated with this flock
    List<FlockAgent> agents = new List<FlockAgent>();
    // The behaviour that the agents in the flock should show
    public FlockBehaviour behaviour;
    // Drive factor of each agent
    [Range(1f, 100f)] public float driveFactor = 10f;
    // Maximum speed of an agent
    float maxSpeed;
    // Neighbours in the given radius that should be considered by an agent
    [Range(1f, 10f)] public float neighbourRadius = 2f;
    // Avoidance radius Multiplier for an agent
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
        /*
         * For each agent in the list, find the neighbours in the given radius
         * Send the reference of this agent, the list of neighbours and reference of this class to the 
         * behaviour object attached with this script.
         * The resultant vector, after the calculation of all the behaviours is multiplied by drive factor
         * and clamped to the maximum speed of the agent.
         * This is then passed to the concerned agent as a message to move in the given direction 
         * with given speed
         */
        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            Vector2 move;

            move = behaviour.calculateMove(agent, context, this);
            move *= driveFactor;
            
            /*
             * Maximum speed is set according to the state of the agent.
             * If agent is in wandering state then the velocity is different 
             * than if the agent is in pursue (attack) mode.
             */
            maxSpeed = agent.MaxVelocity;
            squareMaxSpeed = maxSpeed * maxSpeed;

            if (move.sqrMagnitude > squareMaxSpeed)
            {
                move = move.normalized * maxSpeed;
            }
            
            agent.Move(move);
        }
    }

    /*
     * Returns a list of objects that are nearby to the given agent 
     */
    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        // Find all the colliders that are in the given radius from the given position
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadius);
        /*
         * Given the array of colliders returned from the function
         * Find all the colliders that are not the agent's collider and add them to the list
         * and return the list
         */
        foreach(Collider2D col in contextColliders)
        {
            if(col != agent.AgentCollider)
            {
                context.Add(col.transform);
            }
        }

        return context;
    }

    // Add an agent to this flock
    public void AddAgent(FlockAgent agent)
    {
        agents.Add(agent);
    }

    // Remove an agent from this flock
    public void RemoveAgent(FlockAgent agent)
    {
        // If agent exists, then remove it from the flock and destroy the GameObject for that agent
        if(agents.Contains(agent))
        {
            bool cond = agents.Remove(agent);
            if(cond)
            {
                Destroy(agent.gameObject);
            }
        }
    }
}
