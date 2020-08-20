using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    private bool collided = false;
    private float timePassed = 0f;
    [Tooltip("Positive value to move forward\nNegative to move backward")][SerializeField] float horizontalspeed = 2f;
    public float Horizontalspeed { get { return horizontalspeed; } }
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
            //transform.Translate(horizontalspeed * Vector3.forward * Time.deltaTime);
            if(timePassed < 10f)
            {
                horizontalspeed = 1f;
            }
            else if(timePassed >= 15f && timePassed < 20f)
            {
                horizontalspeed = 2f;
            }
            else
            {
                horizontalspeed = 0f;
            }
            transform.Translate(horizontalspeed * Vector3.up * Time.deltaTime);
            timePassed += Time.deltaTime;
        //}
    }

    void OnCollisionEnter(Collision collision)
    {
        collided = true;
    }
}
