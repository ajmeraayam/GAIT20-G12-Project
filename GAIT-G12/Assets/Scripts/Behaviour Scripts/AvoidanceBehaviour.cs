using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adding scriptable object to the asset menu to create new instances of this object
[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{
    public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // If there are no neighbours, then there will be nothing to avoid
        if(context.Count == 0)
            return Vector2.zero;

        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        // If there is a filter attached with this object, then use that filter to edit the neighbour list
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        /* For each neighbour transform (with a collider) in the list, 
         * Calculate the distance between the agent and the closest point on the perimeter of the collider of that neighbour
         */
        foreach(Transform t in filteredContext)
        {
            Collider2D col2d = t.GetComponent<Collider2D>();
            Vector2 closestPoint = col2d.ClosestPoint(agent.transform.position);
            /*
             * If the distance between the agent and neighnour is less than the allowed closeness
             * then add that distance to the vector
             */
            if((closestPoint - (Vector2) agent.transform.position).sqrMagnitude < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += ((Vector2) agent.transform.position - closestPoint);
            }
        }
        
        // Calculate the average move vector for this agent and return that vector
        if(nAvoid > 0)
            avoidanceMove /= nAvoid;

        return avoidanceMove;
    }
}
