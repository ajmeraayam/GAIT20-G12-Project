using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * Checks if a zombie collides with an opposing agent (Player or Human)
 * On collision it inflicts damage or kills the opposing agent.
 */
public class ZombieCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D other)
    {
        // If zombie collides with player, it reduces the players health
        if(other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerHealth>().ReduceHealth();
        }
        /*
         * If zombie collides with a human, it kills the human and converts it to a zombie
         */
        else if(other.gameObject.tag == "Human")
        {
            // Record the current position and rotation of the human. 
            Vector3 position = other.gameObject.transform.position;
            Quaternion rotation = other.gameObject.transform.rotation;
            // Add the functionality for checking the list of humans that are following the Player and removing the human if it is in the list. Compare gameobjects
            // Remove the human gameobject and replace it with zombie gameobject
            Destroy(other.gameObject);
            // The human is added to a flock. 
            // This is determined by the zombie that killed the human and what flock it belonged to.
            GameObject flockObject = gameObject.GetComponent<FlockAgent>().AgentFlock.gameObject;
            flockObject.GetComponent<RandomSpawner>().InstantiateAgent(position, rotation);
        }
    }
}
