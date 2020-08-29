using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This class generates zombie agents at random position inside the bounds that are provided from the inspector
 */
public class RandomSpawner : MonoBehaviour
{
    // Prefab to be used for spawning new agents
    public FlockAgent agentPrefab;
    private float colliderRadius;
    // Number of agents to be generated
    public int startingCount = 15;
    // Density of the generation
    public float AgentDensity = 0.08f;
    // Radius of the circle in which these agents should be generated
    public float radius = 5f;
    private Vector2 spawningCenter;
    // Suffix that is used in the name of the agent
    private int nameSuffixNumber;
    // Flock object this script belongs to
    private Flock flock;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Flock object 
        flock = GetComponent<Flock>();
        // Radius of the collider of the prefab
        colliderRadius = (agentPrefab.AgentCollider.GetType() == typeof(CircleCollider2D)) ? agentPrefab.GetComponent<CircleCollider2D>().radius : 0f;

        for (int i = 0; i < startingCount; i++)
        {
            bool isLegal = false;
            while(!isLegal)
            {
                // Random location generated for an agent 
                spawningCenter = (Vector2) transform.position + Random.insideUnitCircle * radius * startingCount * AgentDensity;
                /* 
                 * Check if the location generated is not inside a collider
                 * The radius for OverlapCircleAll is chosen to be smaller than size of the collider on 
                 * the agent, so that it checks only for that specified region
                 */
                Collider2D[] colliders = Physics2D.OverlapCircleAll(spawningCenter, colliderRadius);
                if(colliders.Length != 0)
                    continue;
                else
                    isLegal = true;
            }
            
            InstantiateAnAgent(spawningCenter, Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), i);
            nameSuffixNumber = i;
        }
    }

    /*
     * This method is used to Instantiate zombie agent anytime during the game
     */
    public void InstantiateAgent(Vector2 position, Quaternion rotation)
    {
        nameSuffixNumber++;
        InstantiateAnAgent(position, rotation, nameSuffixNumber);
    }

    /*
     * Instantiate the new agent at the given location, using the given prefab, with random rotation
     * and parent as the transform of this gameobject (which handles the Flock and this scripts)
     * Give this agent some name and pass the reference of Flock to this new agent.
     * Add the newly created agent to the list of agents that is stored in the Flock object.
     */
    private void InstantiateAnAgent(Vector2 position, Quaternion rotation, int nameSuffix)
    {
        FlockAgent newAgent = Instantiate(agentPrefab, position, rotation, transform);
        newAgent.name = "Agent " + nameSuffix;
        newAgent.Initialize(flock);
        flock.AddAgent(newAgent);
    }
}
