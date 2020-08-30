using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wander : MonoBehaviour
{


    float Speed = 3;

    Vector3 wayPoint;

    int Range = 10;

    void Start()
    {
        //initialise the target way point
        wander();
    }

    void Update()
    {
        // this is called every frame
        // do move code here
        transform.position += transform.TransformDirection(Vector3.forward) * Speed * Time.deltaTime;
        Debug.Log("position: " + transform.position);
        if ((transform.position - wayPoint).magnitude < 3)
        {
            // when the distance between us and the target is less than 3
            // create a new way point target
            wander();


        }
    }

    void wander()
    {
        // does nothing except pick a new destination to go to

        //wayPoint = new Vector3(Random.Range(transform.position.x - Range, transform.position.x + Range), 1, Random.Range(transform.position.z - Range, transform.position.z + Range));
        wayPoint = new Vector3(Random.Range(transform.position.x - Range, transform.position.x + Range), Random.Range(transform.position.y - Range, transform.position.y + Range), 0);
        //wayPoint.y = 1;
        wayPoint.z = 0;
        Debug.Log("way point: " + wayPoint.ToString());
        // don't need to change direction every frame seeing as you walk in a straight line only
        transform.LookAt(wayPoint);
        Debug.Log(wayPoint + " and " + (transform.position - wayPoint).magnitude);
    }
}

