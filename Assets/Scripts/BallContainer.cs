using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallContainer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    public bool active = false;

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "PlayerBall" && active)
        {
            //Debug.Log("Player lost ball");
            player.GetComponent<Player>().PlayerLostBall();
        }
    }
}
