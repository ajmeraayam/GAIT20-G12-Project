﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FilteredFlockBehaviour
{
    public override Vector3 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if(context.Count == 0)
            return Vector3.zero;

        Vector3 cohesionMove = Vector3.zero;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        foreach(Transform t in filteredContext)
        {
            cohesionMove += t.position;
        }
        cohesionMove /= context.Count;
        cohesionMove -= agent.transform.position;
        return cohesionMove;
    }
}
