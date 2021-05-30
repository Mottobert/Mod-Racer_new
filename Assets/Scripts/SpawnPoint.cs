using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool isBlocked = false;
    public GameObject plane;

    private void Start()
    {
        plane.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SpawnColliderChecker")
        {
            //Debug.Log(this.name + " Blocked");
            isBlocked = true;
            plane.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SpawnColliderChecker")
        {
            //Debug.Log(this.name + " Not Blocked");
            isBlocked = false;
            plane.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        }
    }
}
