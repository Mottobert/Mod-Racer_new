using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    private bool throwBomb = false;
    public Vector3 forward;

    private void Start()
    {
        Invoke("Explode", 1);
    }

    private void Explode()
    {
        this.gameObject.GetComponent<Explosion>().Explode();
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
