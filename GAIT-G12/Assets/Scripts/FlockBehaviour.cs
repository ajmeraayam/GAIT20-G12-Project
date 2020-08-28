using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Abstract class that can be extended by other classes.
 * This class is a child class of ScriptableObject 
 * and hence a prefab can be created for all the classes that extend this class
 * The classes which define a certain behaviour for a flock should extend this class.
 */ 
public abstract class FlockBehaviour : ScriptableObject
{
    // This method will be implemented by all the class that extend this class to calculate the next move
    public abstract Vector2 calculateMove(FlockAgent agent, List<Transform> context, Flock flock);
}
