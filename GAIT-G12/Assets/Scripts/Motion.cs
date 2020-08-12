using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    private bool collided = false;
    [Tooltip("Positive value to move forward\nNegative to move backward")][SerializeField] float horizontalspeed = 2f;
    //Rigidbody rigidbody;
    
    void Start()
    {
        //rigidbody = GetComponent<Rigidbody>();
    }
    // Update is called once per frame
    void Update()
    {
        //if(!collided)
        //{
            transform.Translate(horizontalspeed * Vector3.forward * Time.deltaTime);
        //}
    }

    void OnCollisionEnter(Collision collision)
    {
        collided = true;
    }

    public float Horizontalspeed()
    {
        return horizontalspeed;
    }
}
