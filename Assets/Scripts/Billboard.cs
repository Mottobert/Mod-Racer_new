using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public Transform mainCameraTransform;

    void Start()
    {
        Invoke("SelectPlayerCamera", 0.1f);
    }

    void LateUpdate()
    {
        if (mainCameraTransform)
        {
            //Debug.Log(mainCameraTransform);
            transform.LookAt(transform.position + mainCameraTransform.rotation * Vector3.forward, mainCameraTransform.rotation * Vector3.up);
        }
    }

    private void SelectPlayerCamera()
    {
        GameObject[] photonPlayers = GameObject.FindGameObjectsWithTag("PhotonPlayer");

        //Debug.Log(photonPlayers.Length);
        //Debug.Log(photonPlayers[0].transform.GetChild(0).gameObject.activeInHierarchy);

        foreach (GameObject c in photonPlayers)
        {
            if (c.transform.GetComponent<PhotonPlayer>().PV.IsMine)
            {
                mainCameraTransform = c.transform.GetChild(0).transform;
            }
        }
    }
}
