using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* 
 * Abstract class that can be extended by other classes.
 * This class is a child class of FlockBehaviour (which is a child class of ScriptableObject) 
 * and hence a prefab can be created for all the classes that extend this class
 * The classes which define a certain behaviour for a flock with some filter should extend this class.
 */
public abstract class FilteredFlockBehaviour : FlockBehaviour
{
    // This variable will store the filter that should be implemented for a given behaviour
    public ContextFilter filter;
}
