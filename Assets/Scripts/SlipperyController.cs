using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyController : MonoBehaviour
{
    [SerializeField]
    private WheelCollider rightBack;
    [SerializeField]
    private WheelCollider leftBack;

    private float defaultExtremumSlip = 0.8f;
    private float defaultExtremumValue = 1.4f;
    private float defaultAsymptoteSlip = 0.8f;
    private float defaultAsymptoteValue = 1.2f;

    private void SetWheelsExtremumSlip(float input)
    {
        WheelFrictionCurve tempFCright;
        tempFCright = rightBack.sidewaysFriction;
        tempFCright.extremumSlip = input;
        rightBack.sidewaysFriction = tempFCright;

        WheelFrictionCurve tempFCleft;
        tempFCleft = rightBack.sidewaysFriction;
        tempFCleft.extremumSlip = input;
        rightBack.sidewaysFriction = tempFCleft;
    }

    private void SetWheelsExtremumValue(float input)
    {
        WheelFrictionCurve tempFCright;
        tempFCright = rightBack.sidewaysFriction;
        tempFCright.extremumValue = input;
        rightBack.sidewaysFriction = tempFCright;

        WheelFrictionCurve tempFCleft;
        tempFCleft = rightBack.sidewaysFriction;
        tempFCleft.extremumValue = input;
        rightBack.sidewaysFriction = tempFCleft;
    }

    private void SetWheelsAsymptoteSlip(float input)
    {
        WheelFrictionCurve tempFCright;
        tempFCright = rightBack.sidewaysFriction;
        tempFCright.extremumSlip = input;
        rightBack.sidewaysFriction = tempFCright;

        WheelFrictionCurve tempFCleft;
        tempFCleft = rightBack.sidewaysFriction;
        tempFCleft.extremumSlip = input;
        rightBack.sidewaysFriction = tempFCleft;
    }

    private void SetWheelsAsymptoteValue(float input)
    {
        WheelFrictionCurve tempFCright;
        tempFCright = rightBack.sidewaysFriction;
        tempFCright.extremumValue = input;
        rightBack.sidewaysFriction = tempFCright;

        WheelFrictionCurve tempFCleft;
        tempFCleft = rightBack.sidewaysFriction;
        tempFCleft.extremumValue = input;
        rightBack.sidewaysFriction = tempFCleft;
    }

    private void SetWheelsStiffness(float input)
    {
        WheelFrictionCurve tempFCright;
        tempFCright = rightBack.sidewaysFriction;
        tempFCright.stiffness = input;
        rightBack.sidewaysFriction = tempFCright;

        WheelFrictionCurve tempFCleft;
        tempFCleft = rightBack.sidewaysFriction;
        tempFCleft.stiffness = input;
        rightBack.sidewaysFriction = tempFCleft;
    }

    public void SetWheelsSlipperyOnEnter()
    {
        SetWheelsStiffness(0);
        SetWheelsExtremumSlip(0.1f);
        SetWheelsExtremumValue(0.1f);
        SetWheelsAsymptoteSlip(0.1f);
        SetWheelsAsymptoteValue(0.1f);
    }

    public void ResetWheelsSlipperyOnExit()
    {
        SetWheelsStiffness(1);
        SetWheelsExtremumSlip(defaultExtremumSlip);
        SetWheelsExtremumValue(defaultExtremumValue);
        SetWheelsAsymptoteSlip(defaultAsymptoteSlip);
        SetWheelsAsymptoteValue(defaultAsymptoteValue);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pfuetze")
        {
            //Debug.Log(other.gameObject);
            if (this.GetComponentInParent<Shield>().active)
            {
                Debug.Log("Shield blocked Slippery");
                return;
            }
            Debug.Log("Slippery");
            SetWheelsSlipperyOnEnter();
        }
        else
        {
            ResetWheelsSlipperyOnExit();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Pfuetze")
        {
            if (this.GetComponentInParent<Shield>().active)
            {
                Debug.Log("Shield blocked Not Slippery");
                return;
            }
            Debug.Log("Not Slippery");
            ResetWheelsSlipperyOnExit();
        }
    }
}
