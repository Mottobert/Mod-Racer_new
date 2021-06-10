using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public Transform centerOfMass;
    public float motorTorque = 100f;
    public float maxSteer = 20f;
    public float maxSpeed = 25f;

    public float Steer { get; set; }
    public float Throttle { get; set; }

    private Rigidbody _rigidbody;
    private Wheel[] wheels;

    public float playerSpeed;

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

        ClampMaxSpeed();

        playerSpeed = GetVelocity();
        //Debug.Log(playerSpeed);
    }

    public void ResetVelocity()
    {
        _rigidbody.velocity = Vector3.zero;
    }

    public float GetVelocity()
    {
        if(_rigidbody.velocity != null)
        {
            return _rigidbody.velocity.magnitude;
        }
        else
        {
            return 0;
        }
        
    }

    public void ClampMaxSpeed()
    {
        float speed = GetVelocity();
        if (speed > maxSpeed)
        {
            _rigidbody.velocity *= 0.99f;
        }
    }
}
