using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBehaviourControl : MonoBehaviour
{
    // behaviour script and player object
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        // open wandering
    }

    // Update is called once per frame
    void Update()
    {
        // if in player range, turn off wandering/flee, turn on following
        // if being chased by zombie and not following player, turn off wandering, turn on flee

    }
}
