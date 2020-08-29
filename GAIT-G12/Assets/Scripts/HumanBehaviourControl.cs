using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBehaviourControl : MonoBehaviour
{
    // behaviour script and player object
    public GameObject player;
    private bool isFollowPlayer;
    AILerp pathFollowCompo1;
    AIDestinationSetter pathFollowCompo2;
    Seeker pathFollowCompo3;
    Flee flee;
    Wander wander;


    // Start is called before the first frame update
    void Start()
    {
        // open wandering

        pathFollowCompo1 = this.gameObject.GetComponent<AILerp>();
        pathFollowCompo2 = this.gameObject.GetComponent<AIDestinationSetter>();
        pathFollowCompo3 = this.gameObject.GetComponent<Seeker>();
        flee = this.gameObject.GetComponent<Flee>();
        wander = this.gameObject.GetComponent<Wander>();

        wander.enabled = true;
        pathFollowCompo1.enabled = false;
        pathFollowCompo2.enabled = false;
        pathFollowCompo3.enabled = false;
        isFollowPlayer = false;
        flee.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        bool shouldFollowPlayer = isInPlayerRange();

        // if in player range, turn off wandering/flee, turn on following
        if (shouldFollowPlayer && isFollowPlayer == false)
        {
            wander.enabled = false;
            flee.enabled = false;
            pathFollowCompo1.enabled = true;
            pathFollowCompo2.enabled = true;
            pathFollowCompo3.enabled = true;
            isFollowPlayer = true;
            player.GetComponent<PlayerManager>().humanFollow();
            
        }
        else if (!shouldFollowPlayer && isFollowPlayer != false)
        {
            flee.enabled = false;
            pathFollowCompo1.enabled = false;
            pathFollowCompo2.enabled = false;
            pathFollowCompo3.enabled = false;
            isFollowPlayer = false;
            wander.enabled = true;
            player.GetComponent<PlayerManager>().humanUnfollow();

        }
        

    }

    private bool isInPlayerRange()
    {

        if (Vector3.Distance(transform.position, player.transform.position) < 30)
            return true;
        return false;
    }

    // call by zombie.pursue
    public void startFlee()
    {
        if (!isFollowPlayer)
        {
            wander.enabled = false;
            flee.enabled = true;
        }
    }
}
