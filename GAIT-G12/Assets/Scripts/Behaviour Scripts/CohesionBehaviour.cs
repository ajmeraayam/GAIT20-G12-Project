using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adding scriptable object to the asset menu to create new instances of this object
[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FilteredFlockBehaviour
{
    public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // If there are no neighbours, then there will be nothing to get close to.
        if(context.Count == 0)
            return Vector2.zero;

        Vector2 cohesionMove = Vector2.zero;
        // If there is a filter attached with this object, then use that filter to edit the neighbour list
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        /*
         * For each neighbour transform in the list
         * Add the current position of the neighbour to the given vector
         */
        foreach(Transform t in filteredContext)
        {
            cohesionMove += (Vector2) t.position;
        }
        /* 
         * Calculate the average of the vector 
         * and subtract the position of the agent to find the actual movement needed.
         * Return this vector
         */
        cohesionMove /= context.Count;
        cohesionMove -= (Vector2) agent.transform.position;
        return cohesionMove;
    }
}
