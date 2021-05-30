using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform centerOfMass;
    public float motorTorque = 100f;
    public float maxSteer = 20f;

    public float Steer { get; set; }
    public float Throttle { get; set; }

    private Rigidbody _rigidbody;
    private Wheel[] wheels;

    private void Start()
    {
        wheels = GetComponentsInChildren<Wheel>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.centerOfMass = centerOfMass.localPosition;
    }

    private void FixedUpdate()
    {
        //Debug.Log(Throttle);
        foreach(var wheel in wheels)
        {
            wheel.SteerAngle = Steer * maxSteer;
            wheel.Torgue = Throttle * motorTorque;
        }
    }

    public void ResetVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
    }
}
