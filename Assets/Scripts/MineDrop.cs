using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class MineDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject mine;
    [SerializeField]
    private GameObject dropPosition;
    [SerializeField]
    private int cost = 5;
    private bool isFiring = false;

    public void DropMine()
    {
        if(this.gameObject.GetComponent<Player>().energy >= cost && !isFiring)
        {
            isFiring = true;
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Landmine"), dropPosition.transform.position, dropPosition.transform.rotation);
            //Instantiate(mine, dropPosition.transform.position, dropPosition.transform.rotation);
            this.gameObject.GetComponent<Player>().energy = this.gameObject.GetComponent<Player>().energy - cost;
            this.gameObject.GetComponent<Player>().UpdateEnergyLabel();

            isFiring = false;
        }
    }
}
