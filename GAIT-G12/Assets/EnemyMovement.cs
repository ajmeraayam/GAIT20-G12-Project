using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    private Rigidbody2D rigidBody;
    private GameObject player;
    private Vector2 movement;
    private float speed = 5.0f;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        //use vector subtraction to find vector between player and current object
        Vector2 direction = player.transform.position - gameObject.transform.position;
        direction.Normalize();
        rigidBody.MovePosition((Vector2)gameObject.transform.position + (direction * speed * Time.deltaTime));
    }
}
