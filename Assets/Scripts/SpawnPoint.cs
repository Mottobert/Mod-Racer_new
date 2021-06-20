using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool isBlocked = false;
    public GameObject plane;

    public int collisionCount = 0;

    private void Start()
    {
        plane.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

        //gameObject.GetComponent<PhotonView>().RPC("GetSpawnPointBlockedOnStartForAll", RpcTarget.All, gameObject.GetComponent<PhotonView>().ViewID);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "SpawnColliderChecker" && other.gameObject.GetComponentInParent<PhotonView>().IsMine)
        {
            //Debug.Log(this.name + " Blocked");
            //isBlocked = true;
            //plane.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            collisionCount++;

            gameObject.GetComponent<PhotonView>().RPC("SetSpawnPointBlockedForAll", RpcTarget.AllBufferedViaServer, gameObject.GetComponent<PhotonView>().ViewID, collisionCount);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "SpawnColliderChecker" && other.gameObject.GetComponentInParent<PhotonView>().IsMine)
        {
            collisionCount--;
            if(collisionCount == 0)
            {
                //Debug.Log(this.name + " Not Blocked");
                //isBlocked = false;
                //plane.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

                gameObject.GetComponent<PhotonView>().RPC("SetSpawnPointBlockedForAll", RpcTarget.AllBufferedViaServer, gameObject.GetComponent<PhotonView>().ViewID, collisionCount);
            }
        }
    }

    private void SetSpawnPointBlocked(int collisionCount)
    {
        if(collisionCount != -1)
        {
            this.collisionCount = collisionCount;
        }

        if (this.collisionCount > 0)
        {
            isBlocked = true;
            plane.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        }
        else if(this.collisionCount == 0)
        {
            isBlocked = false;
            plane.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
        }
    }

    private void GetSpawnPointBlockedOnStart(int collisionCount)
    {
        this.collisionCount = collisionCount;


    }

    [PunRPC]
    public void SetSpawnPointBlockedForAll(int viewID, int collisionCount)
    {
        PhotonView.Find(viewID).gameObject.GetComponent<SpawnPoint>().SetSpawnPointBlocked(collisionCount);
    }



    [PunRPC]
    public void GetSpawnPointBlockedOnStartForAll(int viewID)
    {
        //PhotonView.Find(viewID).gameObject.GetComponent<SpawnPoint>().GetSpawnPointBlockedOnStart(collisionCount);

        gameObject.GetComponent<PhotonView>().RPC("SetSpawnPointBlockedForAll", RpcTarget.All, viewID, -1);
    }




    [PunRPC]
    public void SetSpawnPointBlockedOnStartForAll(int viewID)
    {
        PhotonView.Find(viewID).gameObject.GetComponent<SpawnPoint>().GetSpawnPointBlockedOnStart(collisionCount);
    }
}
