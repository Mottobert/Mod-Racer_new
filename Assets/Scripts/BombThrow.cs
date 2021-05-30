using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class BombThrow : MonoBehaviour
{
    [SerializeField]
    private GameObject bomb;
    [SerializeField]
    private GameObject bombThrowPosition;
    [SerializeField]
    private float power = 500.0f;
    [SerializeField]
    private int cost = 5;
    private GameObject instantiatedBomb;
    private bool isFiring = false;

    public void ThrowBomb()
    {
        if(this.gameObject.GetComponent<Player>().energy >= cost && !isFiring)
        {
            isFiring = true;
            instantiatedBomb = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Bombe"), bombThrowPosition.transform.position, bombThrowPosition.transform.rotation);
            //instantiatedBomb = Instantiate(bomb, bombThrowPosition.transform.position, bombThrowPosition.transform.rotation);
            this.gameObject.GetComponent<Player>().energy = this.gameObject.GetComponent<Player>().energy - cost;
            this.gameObject.GetComponent<Player>().UpdateEnergyLabel();

            instantiatedBomb.GetComponent<Bomb>().forward = new Vector3 (this.gameObject.transform.forward.x, this.gameObject.transform.forward.y + 1, this.gameObject.transform.forward.z) * power;
            instantiatedBomb.GetComponent<Bomb>().Throw();
            Invoke("ResetFiring", 0.3f);
        }
    }

    private void ResetFiring()
    {
        isFiring = false;
    }
}
