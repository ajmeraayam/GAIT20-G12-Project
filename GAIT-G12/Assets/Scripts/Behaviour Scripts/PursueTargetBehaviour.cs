using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Pursue Target")]
public class PursueTargetBehaviour : FlockBehaviour
{
    public float radius = 2f;
    float maxForce = 10f;
    public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        GameObject target = null;
        Vector2 velocity = Vector2.zero;
        //Debug.DrawRay(agent.transform.position, (Vector3.up*5), Color.red);
        //Use OverlapCircleNonAlloc
        Collider[] colliders = Physics.OverlapSphere(agent.transform.position, radius);
        
        foreach(Collider collider in colliders)
        {
            if(collider.gameObject.tag == "Player")
            {
                target = collider.gameObject;
                break;
            }
            /*else if(collider.gameObject.CompareTag("Human"))
            {
                target = collider.gameObject;
            }*/
        }

        if(target != null)
        {
            Vector2 pursueMove = (Vector2) target.transform.position - (Vector2) agent.transform.position;
            Vector2 desiredVelocity = pursueMove.normalized * agent.MaxVelocity;
            Vector2 steering = desiredVelocity - agent.CurrentVelocity;
            steering = Vector2.ClampMagnitude(steering, maxForce);
            velocity = Vector2.ClampMagnitude(agent.CurrentVelocity + steering, agent.MaxVelocity);
        }

        return velocity;
    }
}
