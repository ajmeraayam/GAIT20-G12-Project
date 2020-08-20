using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO create static class for the project
public class LeaderFollowing : MonoBehaviour
{
    float MaxVelocity = 10f;
    float MaxForce = 10f;
    private Vector3 velocity;
    private bool isInFlock = false;
    GameObject leader;
    // Start is called before the first frame update
    void Start()
    {
        leader = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(isInFlockingRange(leader))
        {
            if(!isInFlock)
            {
                leader.GetComponent<FlockLeader>().appendToList(gameObject);
                isInFlock = true;
            }

            //Rigidbody rigidbody = leader.GetComponent<Rigidbody>();
            var desiredVelocity = Vector3.Normalize(leader.transform.position - transform.position) * MaxVelocity;
            var steering = desiredVelocity - velocity;
            //steering += followLeader(leader, rigidbody);
            steering += followLeader(leader);

            steering = Vector3.ClampMagnitude(steering, MaxForce);
            //steering /= rigidbody.mass;

            velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
            transform.position += velocity * Time.deltaTime;
            transform.forward = velocity.normalized;
        }
    }

    //arrive, evade, separation
    //Vector3 followLeader(GameObject leader, Rigidbody rigidbody)
    Vector3 followLeader(GameObject leader)
    {
        //Vector3 rVec = rigidbody.velocity;
        Vector3 rVec = velocity;
        Vector3 force = new Vector3();

        rVec.Normalize();
        //Calculate ahead distance
        rVec *= ProjectVars.LEADER_BEHIND_DIST;
        //Position + velocity
        Vector3 ahead = leader.transform.position + rVec;

        //Calculate behind distance
        rVec *= (-1);
        //Position + velocity
        Vector3 behind = leader.transform.position + rVec;

        // If the character is on the leader's sight, add a force
        // to evade the route immediately.
        if (isOnLeaderSight(leader, ahead)) {
            //force += evade(leader, rigidbody);
            force += evade(leader);
        }

        force += separation(leader);
        force += arrive(behind);

        return force;
    }

    Vector3 arrive(Vector3 behind)
    {
        Vector3 desiredVelocity = behind - transform.position;
        float distance = desiredVelocity.magnitude;

        if(distance < ProjectVars.SLOWING_RADIUS)
        {
            desiredVelocity = desiredVelocity.normalized * MaxVelocity * (distance / ProjectVars.SLOWING_RADIUS);
        }
        else
        {
            desiredVelocity = desiredVelocity.normalized * MaxVelocity;
        }

        var steering = desiredVelocity - velocity;
        velocity = Vector3.ClampMagnitude(velocity + steering, MaxVelocity);
        return velocity;
    }

    Vector3 separation(GameObject leader)
    {
        Vector3 force = new Vector3();
        int neighborCount = 0;
        List<GameObject> agentList = leader.GetComponent<FlockLeader>().getListOfAgents();

        foreach(GameObject agent in agentList)
        {
            if(agent != this)
            {
                force = agent.transform.position - transform.position;
                neighborCount++;
            }
        }

        if(neighborCount == 0)
        {
            return Vector3.zero;
        }
        else
        {
            force /= neighborCount;
            force *= -1;
            force.Normalize();
            force *= ProjectVars.MAX_SEPARATION;
            return force;
        }
    }

    private bool isOnLeaderSight(GameObject leader, Vector3 leaderAhead)
    {
        return (Vector3.Distance(leaderAhead, transform.position) <= ProjectVars.LEADER_SIGHT_RADIUS) || (Vector3.Distance(leader.transform.position, transform.position) <= ProjectVars.LEADER_SIGHT_RADIUS);
    } 

    private bool isInFlockingRange(GameObject leader)
    {
        return (Vector3.Distance(leader.transform.position, transform.position) <= ProjectVars.LEADER_SIGHT_RADIUS);
    }
    //Vector3 evade(GameObject leader, Rigidbody leaderRB)
    Vector3 evade(GameObject leader)
    {
        Vector3 distance = leader.transform.position - transform.position;
        float updatesAhead = distance.magnitude / MaxVelocity;
        Vector3 leaderVelocity = leader.GetComponent<Motion>().Horizontalspeed * Vector3.forward;
        Vector3 futurePosition = leader.transform.position + (leaderVelocity * updatesAhead);
        return flee(futurePosition); 
    }

    Vector3 flee(Vector3 position)
    {
        Vector3 desiredVelocity = (transform. position - position).normalized * MaxVelocity;
        return (desiredVelocity - velocity);
    }
}
