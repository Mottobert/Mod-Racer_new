using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class CollisionChecker : MonoBehaviour
{
    public PhotonView PV;
    private bool isReady = true;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EnergyBall" && isReady)
        {
            isReady = false;
            this.GetComponentInParent<Player>().IncreaseEnergy();
            PV.RPC("DeleteEnergy", RpcTarget.MasterClient, other.GetComponent<PhotonView>().ViewID, other.GetComponent<PhotonView>().Owner);
            //DeleteEnergy(other);
            Invoke("MakeReady", 0.5f); // Damit ein EnergyBall nicht öfters registriert wird
        }
    }

    private void MakeReady()
    {
        isReady = true;
    }

    [PunRPC]
    public void DeleteEnergy(int viewID, Photon.Realtime.Player owner)
    {
        if (owner == PV.Owner || PhotonNetwork.IsMasterClient)
        {
            //Debug.Log(PV.Owner);
            //Debug.Log(PhotonNetwork.IsMasterClient);
            
            if (PhotonView.Find(viewID) != null)
            {
                //Debug.Log(PhotonView.Find(viewID).gameObject);
                GameObject target = PhotonView.Find(viewID).gameObject;
                PhotonNetwork.Destroy(target);
            }
        }
    }
}
