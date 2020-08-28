using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Adding scriptable object to the asset menu to create new instances of this object
[CreateAssetMenu(menuName = "Flock/Behaviour/Composite")]
public class CompositeBehaviour : FlockBehaviour
{
    // Array that will hold all the behaviours to create a composite behaviour
    public FlockBehaviour[] behaviours;
    // Array that will hold weights for all the behaviours
    public float[] weights;

    public override Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock)
    {
        // If both the arrays don't have same length, then send an error
        if(weights.Length != behaviours.Length)
        {
            Debug.LogError("Data mismatch in " + name, this);
            return Vector2.zero;
        }

        Vector2 move = Vector2.zero;
        /*
         * For all the behaviours that are in the array, calculate the movement vector
         * and clamp their magnitudes (length) according to the weights provided for that behaviour
         */
        for(int i = 0; i < behaviours.Length; i++)
        {
            Vector2 partialMove = behaviours[i].calculateMove(agent, context, flock) * weights[i];
            if(partialMove != Vector2.zero)
            {
                if(partialMove.sqrMagnitude > (weights[i] * weights[i]))
                {
                    partialMove.Normalize();
                    partialMove *= weights[i];
                }
                // Add all the vectors for the behaviours in one
                move += partialMove;
            }
        }   

        return move;
    }
}
