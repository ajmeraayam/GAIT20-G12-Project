using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float speed;
    private Rigidbody2D rigidBody;
    private Vector2 currentVel;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        currentVel = new Vector2(0.0f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate() {
        Vector3 movement = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0.0f);
        movement.Normalize();
        rigidBody.MovePosition(transform.position + movement * speed * Time.deltaTime);

        //if the player has moved, rotate to match direction
        if(movement.x != 0.0 || movement.y != 0.0) {
            float angle = Mathf.Atan2(movement.y, movement.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
        

    }
}
