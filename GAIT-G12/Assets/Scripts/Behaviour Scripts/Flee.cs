using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flee : MonoBehaviour {
    private Rigidbody2D rigidBody;
    private GameObject enemy;
    
    private Vector2 movement, desiredVel, steering, enemyVel;
    private float speed = 2.0f;
    private float angle, interval;

    public Vector2 currentVel;


    // Start is called before the first frame update
    void Start() {
        rigidBody = this.GetComponent<Rigidbody2D>();
        //enemy = GameObject.FindWithTag("Enemy");
        currentVel = new Vector2(0.0f, 0.0f);
        //enemyVel = enemy.GetComponent<Persue>().currentVel;

    }

    // Update is called once per frame
    void Update() {
        Debug.Log("I am running");
        //Update the enemy's velocity
        enemyVel = enemy.GetComponent<MouseMove>().currentVel; // Used with the Mouse Move
        //enemyVel = enemy.GetComponent<Persue>().currentVel;

        //update interval used to find targets enemy's future position
        interval = (gameObject.transform.position - enemy.transform.position).magnitude / speed;

        //use vector subtraction to find vector between enemy and current object
        Vector2 direction = (Vector2)gameObject.transform.position - ((Vector2)enemy.transform.position + enemyVel * interval);

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        rigidBody.rotation = angle;

        //Normalize direction vector to get the desired velcoty
        direction.Normalize();
        steering = direction - currentVel;
        currentVel = currentVel + steering;

        //Debugging: Move the gameobject up instead of fleeing
        //currentVel = new Vector2(0.0f, 1.0f);
        //rigidBody.MovePosition((Vector2)gameObject.transform.position + Vector2.up * Time.deltaTime * speed);

        rigidBody.MovePosition((Vector2)gameObject.transform.position + (currentVel * speed * Time.deltaTime));

        
        print("Current Vel " + currentVel);
    }

    public void setEnemy(GameObject zombie)
    {
        enemy = zombie;
    }
}