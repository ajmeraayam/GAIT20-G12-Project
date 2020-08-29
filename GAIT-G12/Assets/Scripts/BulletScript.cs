﻿using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
    }

    void OnTriggerEnter2D(Collider2D hitInfo) {
        /*
         * If bullet hits an enemy gameobject, then destroy the enemy and the bullet
         * If bullet hits an obstacle, destroy the bullet
         */
        switch (hitInfo.tag) {
            case "Enemy": 
                hitInfo.gameObject.GetComponent<FlockAgent>().DestroyAgent();
                //increase score by 1 whenever a zombie is killed
                GameManagerScript.Instance.increaseScore(1);
                Destroy(gameObject);
                break;
            case "Obstacle":
                Destroy(gameObject);
                break;

        }       
    }
}
