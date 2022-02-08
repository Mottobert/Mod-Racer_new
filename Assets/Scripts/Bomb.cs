using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private bool throwBomb = false;
    public Vector3 forward;
    public float explosionTimer = 1f;
    public ParticleSystem explosionParticleSystem;
    public MeshRenderer bombMesh;

    private void Start()
    {
        Invoke("Explode", explosionTimer);
    }

    private void Explode()
    {
        this.gameObject.GetComponent<Explosion>().Explode();
        bombMesh.GetComponent<MeshRenderer>().enabled = false;
        explosionParticleSystem.gameObject.transform.rotation = Quaternion.identity;
        explosionParticleSystem.Play();
    }

    public void Throw()
    {
        throwBomb = true;

        Invoke("DisableTrow", 0.3f);
    }

    private void DisableTrow()
    {
        throwBomb = false;
    }

    private void FixedUpdate()
    {
        if (throwBomb)
        {
            this.gameObject.GetComponent<Rigidbody>().AddForce(forward, ForceMode.Impulse);
        }
    }
}
