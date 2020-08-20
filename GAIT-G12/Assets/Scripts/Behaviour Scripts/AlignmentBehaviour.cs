using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FlockBehaviour
{
    public override Vector3 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if(context.Count == 0)
            return agent.transform.up;

        Vector3 alignmentMove = Vector3.zero;
        foreach(Transform t in context)
        {
            alignmentMove += t.transform.up;
        }
        alignmentMove /= context.Count;
        return alignmentMove;
    }
}
