using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour {
    private Rigidbody2D rigidBody;
    private GameObject enemy;
    private Vector2 movement;
    private float speed = 5.0f;
    private float angle;
    // Start is called before the first frame update
    void Start() {
        rigidBody = this.GetComponent<Rigidbody2D>();
        enemy = GameObject.FindWithTag("Enemy");

    }

    // Update is called once per frame
    void Update() {

        //use vector subtraction to find vector between enemy and current object
        Vector2 direction = -(enemy.transform.position - gameObject.transform.position);
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        rigidBody.rotation = angle;
        direction.Normalize();
        rigidBody.MovePosition((Vector2)gameObject.transform.position + (direction * speed * Time.deltaTime));
    }
}