using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adding scriptable object to the asset menu to create new instances of this object
[CreateAssetMenu(menuName = "Flock/Behaviour/Pursue Target")]
public class PursueTargetBehaviour : FlockBehaviour
{
    // Visibility radius of the agent
    public float radius = 2f;
    //Maximum force that should be applied to the agent
    float maxForce = 10f;
    // Maximum speed at which agents should wander around
    [Range(0.1f, 10f)]public float wanderSpeed = 1f;
    // Maximum speed at which agents should pursue some target
    [Range(0.1f, 10f)]public float pursueTargetSpeed = 3f;

    /*
     * Since the visibility radius for agents is different from the radius that is used to define
     * other behaviours like alignment, cohesion and avoidance, the context list isn't used here.
     * Instead, we find colliders in the newly defined visibility radius.
     */
    public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        GameObject target = null;
        Vector2 velocity = Vector2.zero;
        // Search for all the colliders in the given radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(agent.transform.position, radius);

        /*
         * For each collider that is in the given radius, check their tags
         * If a collider has a tag 'Player' or 'Human', then set it as a target.
         * NOTE - If there is a collider in the array with the 'Player' tag, then they are given a preference
         * So in the case where the array has a collider with 'Human' tag and a collider with 'Player' tag,
         * the collider with 'Player' tag will be the preferred target.
         */
        foreach(Collider2D collider in colliders)
        {
            if(collider.gameObject.tag == "Player")
            {
                target = collider.gameObject;
                break;
            }
            else if(collider.gameObject.tag == "Human")
            {
                target = collider.gameObject;
                //target.GetComponent<Flee>().setEnemy(agent);
                //target.GetComponent<HumanBehaviourControl>().startFlee();
            }
        }

        /*
         * If a target is found, then pursue that target with the speed defined in pursueTargetSpeed.
         * If there are no targets in the area, then keep moving at the speed defined in wanderSpeed
         */
        if(target != null)
        {
            /*
            agent.MaxVelocity = pursueTargetSpeed;
            // Calculate the distance between the agent and the target
            Vector2 pursueMove = (Vector2) (target.transform.position - agent.transform.position);
            // Normalize the distance and multiply max velocity
            // Vector2.normalized will give a vector of length 1 with the same direction
            Vector2 desiredVelocity = pursueMove.normalized * agent.MaxVelocity;
            // Steer the agent towards the direction we want it to move.
            Vector2 steering = desiredVelocity - agent.CurrentVelocity;
            // Clamp this steering force with the maximum force that can be applied on an agent
            steering = Vector2.ClampMagnitude(steering, maxForce);
            // Also clamp the velocity of the agent to the maximum velocity defined for the agent
            velocity = Vector2.ClampMagnitude(agent.CurrentVelocity + steering, agent.MaxVelocity);*/
            
            // Change the maximum velocity of the agent
            agent.MaxVelocity = pursueTargetSpeed;
            // Velocity of the target
            Vector2 targetVelocity = target.GetComponent<Rigidbody2D>().velocity;
            // Update interval used to find targets future position
            float interval = (target.transform.position - agent.transform.position).magnitude / pursueTargetSpeed;
            // Use vector subtraction to find vector between target and the agent
            Vector2 direction = ((Vector2)target.transform.position + targetVelocity * interval) - (Vector2)agent.transform.position;
            //Normalize direction vector to get the desired velocity
            direction.Normalize();
            // Steer towards the direction of the movement by target
            Vector2 steering = direction - velocity;
            velocity = velocity + steering;
        }
        else
        {
            // Change the maximum velocity of the agent
            agent.MaxVelocity = wanderSpeed;
        }

        return velocity;
    }
}
