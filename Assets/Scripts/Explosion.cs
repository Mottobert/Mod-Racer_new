using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private float explosionRadius = 15.0f;
    [SerializeField]
    private float explosionPower = 10.0f;
    [SerializeField]
    private float explosionUpforce = 3.0f;

    private bool explode = false;

    private bool shieldActive = false;

    private void FixedUpdate()
    {
        if(explode)
        {
            Detonate();
        }
    }

    public void Detonate()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, explosionRadius);

        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                if ((rb.gameObject.GetComponent<Shield>() && rb.gameObject.GetComponent<Shield>().active) || (rb.gameObject.GetComponent<Player>() && rb.gameObject.GetComponent<Player>().ball && rb == rb.gameObject.GetComponent<Player>().ball.GetComponent<Rigidbody>()))
                {
                    Debug.Log("Shield blocked Explosion");
                    return;
                }

                rb.AddExplosionForce(explosionPower, explosionPos, explosionRadius, explosionUpforce, ForceMode.Impulse);
            }
        }
    }

    public void Explode()
    {
        explode = true;

        Invoke("DisableExplode", 1);
    }

    private void DisableExplode()
    {
        explode = false;
        PhotonNetwork.Destroy(this.gameObject);
    }
}
