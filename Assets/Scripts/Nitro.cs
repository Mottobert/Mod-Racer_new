using System.Collections;
using System.Collections.Generic;
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
            this.gameObject.GetComponent<Player>().energy = this.gameObject.GetComponent<Player>().energy - cost;
            this.gameObject.GetComponent<Player>().UpdateEnergyLabel();
            nitro = true;
            Invoke("DeactivateNitro", duration);
        }
    }

    private void DeactivateNitro()
    {
        nitro = false;
    }
}
