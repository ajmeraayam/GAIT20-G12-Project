using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    float health;
    // Start is called before the first frame update
    void Start()
    {
        health = 100f;
    }

    public void ReduceHealth()
    {
        health -= 5f;
        print("Health - " + health);
        if(health == 0f)
        {
            print("Gameobject destroyed");
            Destroy(gameObject);
        }
    }
}
