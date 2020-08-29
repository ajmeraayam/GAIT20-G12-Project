using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public FlockAgent agentPrefab;
    public int startingCount = 15;
    public float AgentDensity = 0.08f;
    public float radius = 5f;
    // Start is called before the first frame update
    void Start()
    {
        Flock flock = GetComponent<Flock>();
        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(agentPrefab, Random.insideUnitCircle * radius * startingCount * AgentDensity, Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), transform);
            newAgent.name = "Agent " + i;
            newAgent.Initialize(flock);
            flock.AddAgent(newAgent);
        }
    }
}
