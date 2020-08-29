using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Flock))]
public class Leader : MonoBehaviour
{
    //public Motion motionScript;
    Flock flockScript;
    public float visibilityRadius = 5f;

    // Start is called before the first frame update
    void Start()
    {
        flockScript = GetComponent<Flock>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider2D[] agentsInRadius = Physics2D.OverlapCircleAll(transform.position, visibilityRadius);

        foreach(Collider2D c in agentsInRadius)
        {
            if(c.tag == "Human")
            {
                flockScript.AddAgent(c.GetComponent<FlockAgent>());
            }
        }
    }
}
