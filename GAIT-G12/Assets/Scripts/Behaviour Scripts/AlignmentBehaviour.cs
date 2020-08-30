using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adding scriptable object to the asset menu to create new instances of this object
[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FilteredFlockBehaviour
{
    public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // If there are no neighbours, then keep moving in the same direction as before
        if(context.Count == 0)
            return agent.transform.up;

        Vector2 alignmentMove = Vector2.zero;
        // If there is a filter attached with this object, then use that filter to edit the neighbour list
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        // For each neighbour transform in the list, add the direction of their movement to the vector
        foreach(Transform t in filteredContext)
        {
            alignmentMove += (Vector2) t.transform.up;
        }
        // Take an average of all the neighbours alignment and return that vector
        alignmentMove /= context.Count;
        return alignmentMove;
    }
}
