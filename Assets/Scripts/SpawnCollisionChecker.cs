using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollisionChecker : MonoBehaviour
{
    public bool isBlocked = false;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isBlocked = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            isBlocked = false;
        }
    }
}
