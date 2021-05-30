using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vibrate : MonoBehaviour
{
    private Rigidbody rb;
    private bool vibration = false;

    [SerializeField]
    private float intensity = 1.5f;

    private bool shieldActive = false;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    public void ActivateVibration()
    {
        vibration = true;
        Invoke("DeactivateVibration", 1f);
    }

    public void DeactivateVibration()
    {
        vibration = false;
    }

    private void FixedUpdate()
    {
        if (gameObject.GetComponent<Shield>())
        {
            shieldActive = gameObject.GetComponent<Shield>().active;
        }

        if (vibration && !shieldActive)
        {
            float xRandom = Random.Range(0, 50);
            float yRandom = Random.Range(0, 50);
            float zRandom = Random.Range(0, 50);

            float xForce = map(xRandom, 0, 50, -1, 1);
            float yForce = map(yRandom, 0, 50, -1, 1);
            float zForce = map(zRandom, 0, 50, -1, 1);

            Vector3 forceVector = new Vector3(xForce, yForce, zForce);
            rb.AddForce(forceVector * intensity, ForceMode.Impulse);
        }
    }

    private float map(float OldValue, float OldMin, float OldMax, float NewMin, float NewMax)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }
}
