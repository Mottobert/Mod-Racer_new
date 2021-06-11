using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mine : MonoBehaviour
{
    public ParticleSystem explosionParticleSystem;
    public MeshRenderer mineMesh;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            this.gameObject.GetComponent<Explosion>().Explode();
            mineMesh.GetComponent<MeshRenderer>().enabled = false;
            explosionParticleSystem.Play();
        }
    }
}
