using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Pursue Leader")]
public class PursueLeaderBehaviour : FlockBehaviour
{
    float maxForce = 10f;
    public override Vector3 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector3 pursueMove = flock.FollowPoint - agent.transform.position;
        Vector3 velocity = Vector3.zero;
        Vector3 flockSphereCenter = flock.FollowPoint - new Vector3(0f,2f,0f);

        Collider[] colliders = Physics.OverlapSphere(flockSphereCenter, 2f);
        bool inRange = false;

        foreach(Collider c in colliders)
        {
            if(c == agent.AgentCollider)
                inRange = true;
        }

        if(!inRange)
        {
            Vector3 desiredVelocity = pursueMove.normalized * agent.MaxVelocity;
            Vector3 steering = desiredVelocity - agent.CurrentVelocity;
            steering = Vector3.ClampMagnitude(steering, maxForce);
            velocity = Vector3.ClampMagnitude(agent.CurrentVelocity + steering, agent.MaxVelocity);
        } 
        
        return velocity;
        //Update this method. 
    }
}
