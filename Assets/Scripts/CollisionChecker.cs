using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public PhotonView PV;
    private bool isReady = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnergyBall" && PV.IsMine && isReady)
        {
            isReady = false;
            this.GetComponentInParent<Player>().IncreaseEnergy();
            DeleteEnergy(other);
            Invoke("MakeReady", 0.1f); // Damit ein EnergyBall nicht öfters registriert wird
        }
    }

    private void MakeReady()
    {
        isReady = true;
    }
    
    private void DeleteEnergy(Collider collider)
    {
        PhotonNetwork.Destroy(collider.gameObject);
    }
}
