using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    // Point from where bullet should originate
    public Transform shootPoint;
    // Prefab of bullet
    public GameObject bulletPrefab;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1")) {
            shoot();
        }
    }

    private void shoot() 
    {
        Instantiate(bulletPrefab, shootPoint.position, shootPoint.rotation);
    }
}
