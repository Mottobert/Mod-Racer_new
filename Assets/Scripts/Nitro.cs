using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Nitro : MonoBehaviour
{
    private Rigidbody rb;
    private bool nitro = false;

    [SerializeField]
    private float intensity = 2f;
    [SerializeField]
    private float duration = 1f;
    [SerializeField]
    private int cost = 5;

    [SerializeField]
    private ParticleSystem nitroParticleSystem1;
    [SerializeField]
    private ParticleSystem nitroParticleSystem2;

    [SerializeField]
    private GameObject player;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if (nitro)
        {
            rb.AddForce(transform.forward * intensity, ForceMode.Impulse);
        }
    }

    public void ActivateNitro()
    {
        if (this.gameObject.GetComponent<Player>().energy >= cost)
        {
            player.GetComponent<PhotonView>().RPC("ActivateNitroParticleSystemForPlayer", RpcTarget.All, player.GetComponent<PhotonView>().ViewID);
            ActivateNitroParticleSystem();
            this.gameObject.GetComponent<Player>().energy = this.gameObject.GetComponent<Player>().energy - cost;
            this.gameObject.GetComponent<Player>().UpdateEnergyLabel();
            nitro = true;
            
        }
    }

    private void DeactivateNitro()
    {
        nitro = false;
        nitroParticleSystem1.Stop();
        nitroParticleSystem2.Stop();
    }

    private void ActivateNitroParticleSystem()
    {
        nitroParticleSystem1.Play();
        nitroParticleSystem2.Play();
        Invoke("DeactivateNitro", duration);
    }

    [PunRPC]
    public void ActivateNitroParticleSystemForPlayer(int viewID)
    {
        PhotonView.Find(viewID).gameObject.GetComponent<Nitro>().ActivateNitroParticleSystem();
        //player.GetComponent<Nitro>().ActivateNitroParticleSystem();
    }
}
