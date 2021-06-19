using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class PfuetzeDrop : MonoBehaviour
{
    [SerializeField]
    private GameObject pfuetze;
    [SerializeField]
    private GameObject dropPosition;
    [SerializeField]
    private int cost = 5;
    [SerializeField]
    private float despawnTime = 5f;
    private bool isFiring = false;

    private GameObject pfuetzeInstance;

    public void DropPfuetze()
    {
        if (this.gameObject.GetComponent<Player>().energy >= cost && !isFiring)
        {
            isFiring = true;
            pfuetzeInstance = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PfuetzeSpawnPoint"), dropPosition.transform.position, dropPosition.transform.rotation);
            //Instantiate(pfuetze, dropPosition.transform.position, dropPosition.transform.rotation);
            this.gameObject.GetComponent<Player>().energy = this.gameObject.GetComponent<Player>().energy - cost;
            this.gameObject.GetComponent<Player>().UpdateEnergyLabel();

            isFiring = false;

            Invoke("DeletePfuetze", despawnTime);
        }
    }

    private void DeletePfuetze()
    {
        PhotonNetwork.Destroy(pfuetzeInstance);
    }
}
