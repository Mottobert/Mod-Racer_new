using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Electroshock : MonoBehaviour
{
    [SerializeField]
    private float shockRadius = 15.0f;
    [SerializeField]
    private int cost = 5;

    [SerializeField]
    private ParticleSystem electricityParticleSystem;
    [SerializeField]
    private GameObject player;

    public void ActivateElectroshock()
    {
        if (this.gameObject.GetComponent<Player>().energy >= cost)
        {
            player.GetComponent<PhotonView>().RPC("ActivateElectricityParticleSystemForPlayer", RpcTarget.All, player.GetComponent<PhotonView>().ViewID);
            ActivateElectricityParticleSystem();
            this.gameObject.GetComponent<Player>().energy = this.gameObject.GetComponent<Player>().energy - cost;
            this.gameObject.GetComponent<Player>().UpdateEnergyLabel();
            Vector3 shockPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(shockPos, shockRadius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();

                if (rb != null && rb != this.gameObject.GetComponent<Rigidbody>() && rb.gameObject.GetComponent<Vibrate>())
                {
                    //Debug.Log(rb.GetComponent<PhotonView>());
                    rb.gameObject.GetComponent<Vibrate>().ActivateVibration();
                    rb.GetComponent<PhotonView>().RPC("ActivateVibrationExternal", RpcTarget.All, rb.GetComponent<PhotonView>().ViewID);
                }
            }
        }
    }

    private void ActivateElectricityParticleSystem()
    {
        electricityParticleSystem.Play();
        Invoke("DeactivateElectricityParticleSystem", 1.1f);
    }

    private void DeactivateElectricityParticleSystem()
    {
        electricityParticleSystem.Stop();
    }

    [PunRPC]
    public void ActivateElectricityParticleSystemForPlayer(int viewID)
    {
        //Debug.Log("Test Elektro");
        PhotonView.Find(viewID).gameObject.GetComponent<Electroshock>().ActivateElectricityParticleSystem();
    }
}
