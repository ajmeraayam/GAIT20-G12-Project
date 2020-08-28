using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public FlockAgent agentPrefab;
    public int startingCount = 15;
    public float AgentDensity = 0.08f;
    public float radius = 5f;
    Vector2 spawningCenter;
    // Start is called before the first frame update
    void Start()
    {
        Flock flock = GetComponent<Flock>();
        for (int i = 0; i < startingCount; i++)
        {
            bool isLegal = false;
            while(!isLegal)
            {
                spawningCenter = (Vector2) transform.position + Random.insideUnitCircle * radius * startingCount * AgentDensity;
                Collider2D[] colliders = Physics2D.OverlapCircleAll(spawningCenter, 0.25f);
                if(colliders.Length != 0)
                    continue;
                else
                    isLegal = true;
            }
            
            FlockAgent newAgent = Instantiate(agentPrefab, spawningCenter, Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), transform);
            newAgent.name = "Agent " + i;
            newAgent.Initialize(flock);
            flock.AddAgent(newAgent);
        }
    }
}
