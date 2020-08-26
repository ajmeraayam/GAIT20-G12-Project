using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{
    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviour behaviour;
    [Range(1, 250)] public int startCount = 25;
    const float agentDensity = 0.08f;
    [Range(1f, 100f)] public float driveFactor = 10f;
    float maxSpeed;
    [Range(1f, 10f)] public float neighbourRadius = 2f;
    [Range(0f, 1f)] public float avoidanceRadiusMultiplier = 0.5f;

    float squareMaxSpeed;
    float squareNeighbourRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return squareAvoidanceRadius; } }
    Vector2 followPoint;
    public Vector2 FollowPoint { get { return followPoint; } }
    string tag_local = null; 
    float maxForce = 10f;
    
    void Start()
    {
        squareNeighbourRadius = neighbourRadius * neighbourRadius;
        squareAvoidanceRadius = squareNeighbourRadius * avoidanceRadiusMultiplier * avoidanceRadiusMultiplier;
        //
        if(gameObject.CompareTag("Player"))
        {
            tag_local = "Player";
            followPoint = new Vector2(transform.localPosition.x, transform.localPosition.y - 1f);
        }
        else
        {
            tag_local = null;
            followPoint = Vector2.zero;
            /*for (int i = 0; i < startCount; i++)
            {
                FlockAgent newAgent = Instantiate(agentPrefab, Random.insideUnitCircle * startCount * agentDensity, Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), transform);
                newAgent.name = "Agent " + i;
                newAgent.Initialize(this);
                agents.Add(newAgent);
            }*/
        }
    }

    void Update()
    {
        if(string.Equals(tag_local, "Player"))
        {
            //CHANGE THIS!!!! Use leader speed instead of scripted speed
            maxSpeed = (GetComponent<Motion>().Horizontalspeed == 0f) ? 2f : GetComponent<Motion>().Horizontalspeed;
            followPoint = new Vector2(transform.localPosition.x, transform.localPosition.y - 0.5f);
            squareMaxSpeed = maxSpeed * maxSpeed;
        }
        else
        {
            //Shift desiredveclocity to flock agents or some other class, so only the agents close to the target approach the target.
            /*Physics2D.OverlapCircleAll();
            GameObject target = GameObject.FindWithTag("Target");
            Vector2 flockCenter = findCenter();

            Vector2 distance = (Vector2) target.transform.position - flockCenter;
            Vector2 desiredVelocity = distance.normalized * maxSpeed;*/
        }

        foreach(FlockAgent agent in agents)
        {
            List<Transform> context = GetNearbyObjects(agent);
            Vector2 move;

            if(string.Equals(tag_local, "Player"))
            {
                float dist = ((Vector2)agent.transform.position - followPoint).magnitude;
                //CHANGE THIS!! Use original speed instead of scripted speed
                if(dist < 1f && GetComponent<Motion>().Horizontalspeed == 0f)
                    move = Vector2.zero;
                else
                {
                    move = behaviour.calculateMove(agent, context, this);
                    move *= driveFactor;
                    if(move.sqrMagnitude > squareMaxSpeed)
                    {
                        move = move.normalized * maxSpeed;
                    }
                }
            }
            else
            {
                move = behaviour.calculateMove(agent, context, this);
                //if(distance.magnitude > 0.1f)
                //    move += seekForce(agent, desiredVelocity); 
                move *= driveFactor;
                
                maxSpeed = agent.MaxVelocity;
                squareMaxSpeed = maxSpeed * maxSpeed;

                if (move.sqrMagnitude > squareMaxSpeed)
                {
                    move = move.normalized * maxSpeed;
                }
            }
            
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        //Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighbourRadius);
        Collider2D[] contextColliders = Physics2D.OverlapCircleAll(agent.transform.position, neighbourRadius);
        foreach(Collider2D col in contextColliders)
        {
            print(col);
            //test after removing player tag condition
            if(col != agent.AgentCollider)
            {
                context.Add(col.transform);
            }
        }

        return context;
    }

    public void AddAgent(FlockAgent agent)
    {
        //Change tags. Remove tag assignment line before submission
        //agent.transform.gameObject.tag = "In Flock";
        agents.Add(agent);
    }

    /*private Vector2 seekForce(FlockAgent agent, Vector2 desiredVelocity)
    {
        Vector2 steering = desiredVelocity - agent.CurrentVelocity;
        steering = Vector2.ClampMagnitude(steering, maxForce);
        return Vector2.ClampMagnitude(agent.CurrentVelocity + steering, agent.MaxVelocity);
    }
    private Vector2 findCenter()
    {
        Vector2 center = new Vector2(0f, 0f);
        float count = 0f;
        
        foreach(FlockAgent agent in agents)
        {
            center += (Vector2) agent.gameObject.transform.position;
            count++;
        }
        return (center / count);
    }*/
}
