using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Persue : MonoBehaviour
{
    private Rigidbody2D rigidBody;
    private GameObject target;
    private Vector2 desiredVel, steering, targetVel;
    private float speed = 5.0f;
    private float angle, interval;

    public Vector2 currentVel;

    // Start is called before the first frame update
    void Start() {
        rigidBody = this.GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Human");
        
        currentVel.x = 0.0f;
        currentVel.y = 0.0f;
        targetVel = target.GetComponent<Flee>().currentVel;
    }

    // Update is called once per frame
    void Update() {
        persue(target);
    }

    private void persue(GameObject target) {
        //Update the targets velocity
        //targetVel = target.GetComponent<MouseMove>().currentVel; // Used with the Mouse Move
        targetVel = target.GetComponent<Flee>().currentVel;
        //update interval used to find targets future position
        interval = (target.transform.position - gameObject.transform.position).magnitude / speed;

        //use vector subtraction to find vector between player and current object
        Vector2 direction = ((Vector2)target.transform.position + targetVel * interval)
            - (Vector2)gameObject.transform.position;

        //find angle needed to rotate toward the target
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        rigidBody.rotation = angle;

        //Normalize direction vector to get the desired velocity
        direction.Normalize();
        steering = direction - currentVel;
        currentVel = currentVel + steering;

        rigidBody.MovePosition((Vector2)gameObject.transform.position + (currentVel * speed * Time.deltaTime));
    }
}
