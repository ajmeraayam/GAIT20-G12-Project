using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{
    AIDestinationSetter AISetter;
    private float time;
    int Range = 10;

    private void Start()
    {
        AISetter = this.gameObject.GetComponent<AIDestinationSetter>();
    }

    private void Update()
    {
        if (time > 5)
        {
            gameObject.GetComponent<HumanBehaviourControl>().wanderTarget.transform.position = new Vector3(Random.Range(transform.position.x - Range, transform.position.x + Range), Random.Range(transform.position.y - Range, transform.position.y + Range), 0);
            time = 0;
        }
        else
        {
            time+=Time.deltaTime;
        }
        
    }

  
}


