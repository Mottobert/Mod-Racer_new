using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnCollisionChecker : MonoBehaviour
{
    public bool isBlocked = false;
    private int collisionCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            isBlocked = true;
            collisionCount++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            collisionCount--;

            if(collisionCount == 0)
            {
                isBlocked = false;
            }
        }
    }
}
