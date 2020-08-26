using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockLeader : MonoBehaviour
{
    List<GameObject> list;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        list = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        //velocity = transform.forward;
        //velocity = Vector3.ClampMagnitude( velocity, 2.0f );
        //transform.position += velocity * Time.deltaTime;
        //transform.forward = velocity.normalized;
    }

    public List<GameObject> getListOfAgents()
    {
        return list;
    }

    public void appendToList(GameObject agent)
    {
        list.Add(agent);
    }
}
