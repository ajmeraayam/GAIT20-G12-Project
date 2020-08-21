using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviour behaviour;
    [Range(10, 50)] public int startCount = 25;
    const float agentDensity = 0.3f;
    [Range(1f, 100f)] public float driveFactor = 10f;
    //MaxSpeed is for flock 
    //float maxSpeed;
    [Range(1f, 10f)] public float maxSpeed = 2f;
    //[Range(1f, 10f)] public float neighbourRadius = 5f;
    [Range(1f, 10f)] public float neighbourRadius = 1.5f;
    [Range(0f, 1f)] public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    Vector3 followPoint;
    public Vector3 FollowPoint { get { return followPoint; } }

    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;

        for (int i = 0; i < startCount; i++)
        {
            FlockAgent newAgent = Instantiate(agentPrefab, Random.insideUnitCircle * startCount * agentDensity, Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), transform);
            newAgent.name = "Agent " + i;
            //newAgent.Initialize(this);
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);

            //FOR DEMO ONLY
            //agent.GetComponentInChildren<SpriteRenderer>().color = Color.Lerp(Color.white, Color.red, context.Count / 6f);

            Vector3 move = behaviour.calculateMove(agent, context, this);
            move *= driveFactor;
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
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighbourRadius);
        foreach (Collider c in contextColliders)
        {
            if (c != agent.AgentCollider)
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

    /*void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        followPoint = new Vector3(transform.localPosition.x, transform.localPosition.y - 2f, transform.localPosition.z);
    }

    void Update()
    {
        maxSpeed = GetComponent<Motion>().Horizontalspeed;
        followPoint = new Vector3(transform.localPosition.x, transform.localPosition.y - 2f, transform.localPosition.z);

        if(GetComponent<Motion>().Horizontalspeed != 0f)
        {
            foreach(FlockAgent agent in agents)
            {
                List<Transform> context = GetNearbyObjects(agent);
                Vector3 move = behaviour.calculateMove(agent, context, this);
                move *= driveFactor;
                if(move.sqrMagnitude > squareMaxSpeed)
                {
                    move = move.normalized * maxSpeed;
                }
                agent.Move(move);
            }    
        }
    }
    
    public void AddAgent(FlockAgent agent)
    {
        //Change tags
        agent.transform.gameObject.tag = "In Flock";
        agents.Add(agent);
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighbourRadius);
        foreach(Collider col in contextColliders)
        {
            if(col != agent.AgentCollider)
            {
                context.Add(col.transform);
            }
        }

        return context;
    }*/
}
