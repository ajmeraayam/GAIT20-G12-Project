
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
        //transform.position += transform.TransformDirection(Vector3.up) * Speed * Time.deltaTime;
        transform.position += (Vector3)transform.TransformDirection(Vector2.up) * Speed * Time.deltaTime;
        //Vector3 pos = transform.position + transform.TransformDirection(Vector3.up) * Speed * Time.deltaTime;
        //pos.z = 0;
        //transform.position = pos;
        

        Debug.Log("position: " + transform.position);
        
        if ((transform.position - (Vector3)wayPoint).magnitude < 3)
        {
            // when the distance between us and the target is less than 3
            // create a new way point target
            wander();


        }
    }

    void wander()
    {
        // does nothing except pick a new destination to go to
        wayPoint = new Vector2(Random.Range(transform.position.x - Range, transform.position.x + Range), Random.Range(transform.position.y - Range, transform.position.y + Range));
        transform.LookAt((Vector3)wayPoint);

        //wayPoint = new Vector3(Random.Range(transform.position.x - Range, transform.position.x + Range), 1, Random.Range(transform.position.z - Range, transform.position.z + Range));
        //wayPoint = new Vector3(Random.Range(transform.position.x - Range, transform.position.x + Range), Random.Range(transform.position.y - Range, transform.position.y + Range), 0);
        //wayPoint.y = 1;
        //wayPoint.z = 0;
        //Debug.Log("way point: " + wayPoint.ToString());
        // don't need to change direction every frame seeing as you walk in a straight line only
        //transform.LookAt(wayPoint);
        //Vector3 pos = transform.position;
        //pos.z = 0;
        //transform.position = pos;
        //Debug.Log("LockAt: " + transform.position);
        //Debug.Log(wayPoint + " and " + (transform.position - wayPoint).magnitude);
    }
}


