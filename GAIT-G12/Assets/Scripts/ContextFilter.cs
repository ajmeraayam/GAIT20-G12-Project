using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Abstract class that can be extended by other classes.
 * This class is a child class of ScriptableObject
 * and hence a prefab can be created for all the classes that extend this class
 * The classes which define a filter for the neighbour list should extend this class
 */
public abstract class ContextFilter : ScriptableObject
{
    // This method will filter the list of neighbours according to the definition of that filter.
    public abstract List<Transform> Filter(FlockAgent agent, List<Transform> original);
}
