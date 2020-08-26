using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Pursue Leader")]
public class PursueLeaderBehaviour : FlockBehaviour
{
    float maxForce = 10f;
    public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        Vector2 pursueMove = flock.FollowPoint - (Vector2) agent.transform.position;
        Vector2 velocity = Vector2.zero;
        Vector3 flockSphereCenter = flock.FollowPoint;
        //- new Vector3(0f,2f,0f)
        Collider[] colliders = Physics.OverlapSphere(flockSphereCenter, 1f);
        bool inRange = false;

        foreach(Collider c in colliders)
        {
            if(c == agent.AgentCollider)
                inRange = true;
        }

        if(!inRange)
        {
            Vector2 desiredVelocity = pursueMove.normalized * agent.MaxVelocity;
            Vector2 steering = desiredVelocity - agent.CurrentVelocity;
            steering = Vector2.ClampMagnitude(steering, maxForce);
            velocity = Vector2.ClampMagnitude(agent.CurrentVelocity + steering, agent.MaxVelocity);
        } 
        
        return velocity;
        //Update this method. 
    }
}
