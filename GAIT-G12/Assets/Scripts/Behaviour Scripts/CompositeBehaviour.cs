﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
    public FlockBehaviour[] behaviours;
    public float[] weights;
    public override Vector3 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        if(weights.Length != behaviours.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector3.zero;
        }

        Vector3 move = Vector3.zero;
        for(int i = 0; i < behaviours.Length; i++)
        {
            Vector3 partialMove = behaviours[i].calculateMove(agent, context, flock) * weights[i];
            if(partialMove != Vector3.zero)
            {
                if(partialMove.sqrMagnitude > (weights[i] * weights[i]))
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }

                move += partialMove;
            }
        }   
        /*target = flock.Target();
        for(int i = 0; i < context.Count; i++)
        {

        }*/

        return move;
    }
}
