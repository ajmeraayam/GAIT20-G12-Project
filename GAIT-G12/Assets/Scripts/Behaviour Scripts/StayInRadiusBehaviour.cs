using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adding scriptable object to the asset menu to create new instances of this object
[CreateAssetMenu(menuName = "Flock/Behaviour/Stay In Radius")]
public class StayInRadiusBehaviour : FlockBehaviour
{
    // Center of the circle in which the agents should stay
    public Vector2 center;
    // Radius of that circle
    public float radius;
    public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // Find the distance between agent and the center point
        Vector2 centerOffset = center - (Vector2) agent.transform.position;
        // Find the distance in percentage
        float t = centerOffset.magnitude / radius;

        /* 
         * If an agent is at a distance of < 90% from the center then no need to change the current direction
         * Otherwise, change the direction to come back into the circle
         */
        if(t < 0.9f)
        {
            return Vector2.zero;
        }

        return centerOffset * t * t;
    }
}
