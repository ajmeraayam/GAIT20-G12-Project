using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adding scriptable object to the asset menu to create new instances of this object
[CreateAssetMenu(menuName = "Flock/Filter/Physics Layer")]
public class PhysicsLayerFilter : ContextFilter
{
    // Use the attached layermask to compare with the mask of the incoming list of objects
    public LayerMask mask;

    public override List<Transform> Filter(FlockAgent agent, List<Transform> original)
    {
        List<Transform> filtered = new List<Transform>();

        /*
         * For each transform in the original list,
         * Check if they are on Layer 8 which is the Obstacle layer
         * If they are, then add them to the filtered list and return the list when all the items are checked
         */
        foreach(Transform item in original)
        {
            if(mask == (mask | 1 << item.gameObject.layer))
            {
                filtered.Add(item);
            }
        }
        
        return filtered;
    }
}
