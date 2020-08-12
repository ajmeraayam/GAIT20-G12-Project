using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseMove : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    private GameObject target;
    private Vector2 desiredVel, steering;
    private float speed = 5.0f;
    private float angle;

    public Vector2 currentVel;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        currentVel.x = 0.0f;
        currentVel.y = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        //use vector subtraction to find vector between player and current object
        Vector2 direction = (Vector2)mousePos
            -(Vector2)gameObject.transform.position;

        //find angle needed to rotate toward the target
        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90.0f;
        rigidBody.rotation = angle;

        //Normalize direction vector to get the desired velcoty
        direction.Normalize();
        steering = direction - currentVel;
        currentVel = currentVel + steering;
        rigidBody.MovePosition((Vector2)gameObject.transform.position + (currentVel * speed * Time.deltaTime));

    }
}
