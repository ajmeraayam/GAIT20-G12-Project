using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{

    private int humanFollowing;
    // Start is called before the first frame update
    void Start()
    {
        humanFollowing = 0;
    }

    public void humanFollow()
    {
        humanFollowing++;
    }

    public void humanUnfollow()
    {
        humanFollowing--;
    }

    // if following human dead
    public void humanDead()
    {
        humanFollowing--;
    }


}
