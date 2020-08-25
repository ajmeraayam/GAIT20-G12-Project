using System.Collections;
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

        switch (hitInfo.tag) {
            case "Enemy":
                Destroy(hitInfo.gameObject);
                Destroy(gameObject);
                break;
            case "Obstacle":
                Destroy(gameObject);
                break;

        }
        //if (hitInfo.tag != "Player") {
        //    Destroy(gameObject);
        //} else if(hitInfo)

               
    }

}
