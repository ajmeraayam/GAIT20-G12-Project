using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanBehaviourControl : MonoBehaviour
{
    // behaviour script and player object
    public GameObject player;
    private bool pathFinding;
    AILerp pathFollowCompo1;
    AIDestinationSetter pathFollowCompo2;
    Seeker pathFollowCompo3;
    AIDestinationSetter AISetter;
    Flee flee;
    Wander wander;
    private GameObject gameManager;
    GameObject wanderTarget;
    private bool isFollowPlayer;
    private bool isFlee;


    // Start is called before the first frame update
    void Start()
    {
        // open wandering
        gameManager = GameObject.Find("GameManager");
        wanderTarget = GameObject.Find("WanderTarget");
        pathFollowCompo1 = this.gameObject.GetComponent<AILerp>();
        pathFollowCompo2 = this.gameObject.GetComponent<AIDestinationSetter>();
        pathFollowCompo3 = this.gameObject.GetComponent<Seeker>();
        flee = this.gameObject.GetComponent<Flee>();
        wander = this.gameObject.GetComponent<Wander>();

        AISetter = this.gameObject.GetComponent<AIDestinationSetter>();
        wander.enabled = true;
        AISetter.target = wanderTarget.transform;


        //pathFollowCompo1.enabled = false;
        //pathFollowCompo2.enabled = false;
        //pathFollowCompo3.enabled = false;
        pathFinding = true;
        isFollowPlayer = false;
        flee.enabled = false;
        /*
        wander.enabled = true;
        pathFollowCompo1.enabled = false;
        pathFollowCompo2.enabled = false;
        pathFollowCompo3.enabled = false;
        isFollowPlayer = false;
        flee.enabled = false;
        */
    }

    // Update is called once per frame
    void Update()
    {
        bool shouldFollowPlayer = isInPlayerRange();
        if (!isFlee && pathFinding!=true)
        {
            pathFollowCompo1.enabled = true;
            pathFollowCompo2.enabled = true;
            pathFollowCompo3.enabled = true;
        }

        // if in player range, turn off wandering/flee, turn on following
        if (shouldFollowPlayer)
        {
            wander.enabled = false;
            flee.enabled = false;
            
            AISetter.target = player.transform;

            isFollowPlayer = true;
            gameManager.GetComponent<GameManagerScript>().AddHumanToList(gameObject);


        }
        else if (!shouldFollowPlayer)
        {
            flee.enabled = false;
            //pathFollowCompo1.enabled = false;
            //pathFollowCompo2.enabled = false;
            //pathFollowCompo3.enabled = false;
            isFollowPlayer = false;
            wander.enabled = true;
            AISetter.target = wanderTarget.transform;
            gameManager.GetComponent<GameManagerScript>().RemoveHumanFromList(gameObject);
            
        }



    }

    private bool isInPlayerRange()
    {
        if (Vector3.Distance(transform.position, player.transform.position) < 20)
            return true;
        return false;
    }

    // call by zombie.pursue
    public void startFlee()
    {
        if (!isFollowPlayer)
        {
            wander.enabled = false;
            pathFollowCompo1.enabled = false;
            pathFollowCompo2.enabled = false;
            pathFollowCompo3.enabled = false;
            flee.enabled = true;
            pathFinding = false;
        }
    }
}
