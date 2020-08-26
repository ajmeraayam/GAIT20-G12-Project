using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public FlockAgent agentPrefab;
    int startingCount = 15;
    float AgentDensity = 0.08f;
    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < startingCount; i++)
        {
            FlockAgent newAgent = Instantiate(agentPrefab, Random.insideUnitCircle * 5 * startingCount * AgentDensity, Quaternion.Euler(Vector3.forward * Random.Range(0f, 360f)), transform);
            newAgent.name = "Agent " + i;
            //newAgent.Initialize(this);
            //agents.Add(newAgent);
            GetComponent<Flock>().AddAgent(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
