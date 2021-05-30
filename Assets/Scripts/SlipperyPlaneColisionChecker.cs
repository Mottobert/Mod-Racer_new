using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlipperyPlaneColisionChecker : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SlipperyGroundChecker")
        {
            //Debug.Log(other.gameObject);
            if (other.GetComponentInParent<Shield>().active)
            {
                Debug.Log("Shield blocked Slippery");
                return;
            }
            Debug.Log("Slippery");
            other.gameObject.GetComponentInChildren<SlipperyController>().SetWheelsSlipperyOnEnter();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SlipperyGroundChecker")
        {
            if (other.GetComponentInParent<Shield>().active)
            {
                Debug.Log("Shield blocked Not Slippery");
                return;
            }
            Debug.Log("Not Slippery");
            other.gameObject.GetComponentInChildren<SlipperyController>().ResetWheelsSlipperyOnExit();
        }
    }
}
