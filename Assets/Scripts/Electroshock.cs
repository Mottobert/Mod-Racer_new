using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electroshock : MonoBehaviour
{
    [SerializeField]
    private float shockRadius = 15.0f;
    [SerializeField]
    private int cost = 5;

    public void ActivateElectroshock()
    {
        if (this.gameObject.GetComponent<Player>().energy >= cost)
        {
            this.gameObject.GetComponent<Player>().energy = this.gameObject.GetComponent<Player>().energy - cost;
            this.gameObject.GetComponent<Player>().UpdateEnergyLabel();
            Vector3 shockPos = transform.position;
            Collider[] colliders = Physics.OverlapSphere(shockPos, shockRadius);
            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.gameObject.GetComponent<Rigidbody>();

                if (rb != null && rb != this.gameObject.GetComponent<Rigidbody>() && rb.gameObject.GetComponent<Vibrate>())
                {
                    rb.gameObject.GetComponent<Vibrate>().ActivateVibration();
                }
            }
        }
    }
}
