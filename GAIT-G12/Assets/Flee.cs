using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour {
    private Rigidbody2D rigidBody;
    private GameObject enemy;
    private Vector2 movement, currentVel, desiredVel, steering;
    private float speed = 5.0f;
    private float angle;
    // Start is called before the first frame update
    void Start() {
        rigidBody = this.GetComponent<Rigidbody2D>();
        enemy = GameObject.FindWithTag("Enemy");
        currentVel.x = 0.0f;
        currentVel.y = 0.0f;

    }

    // Update is called once per frame
    void Update() {

        //use vector subtraction to find vector between enemy and current object
        Vector2 direction = gameObject.transform.position - enemy.transform.position;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        rigidBody.rotation = angle;

        //Normalize direction vector to get the desired velcoty
        direction.Normalize();
        steering = direction - currentVel;
        currentVel = currentVel + steering;
        rigidBody.MovePosition((Vector2)gameObject.transform.position + (currentVel * speed * Time.deltaTime));
    }
}