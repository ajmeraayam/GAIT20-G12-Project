﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Avoidance")]
public class AvoidanceBehaviour : FilteredFlockBehaviour
{
    public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if(context.Count == 0)
            return Vector2.zero;

        Vector2 avoidanceMove = Vector2.zero;
        int nAvoid = 0;
        List<Transform> filteredContext = (filter == null) ? context : filter.Filter(agent, context);

        foreach(Transform t in filteredContext)
        {
            if(t.gameObject.name == "Hills_9")
                flock.printMessage("Name - " + t.gameObject.name + "Position - " + t.position + ", Tag - " + t.tag);
            //if((t.position - agent.transform.position).sqrMagnitude < flock.SquareAvoidanceRadius)
            if(((Vector2) t.position - (Vector2) agent.transform.position).sqrMagnitude < flock.SquareAvoidanceRadius)
            {
                nAvoid++;
                avoidanceMove += (Vector2) (agent.transform.position - t.position);
            }
        }
        
        if(nAvoid > 0)
            avoidanceMove /= nAvoid;

        return avoidanceMove;
    }
}
