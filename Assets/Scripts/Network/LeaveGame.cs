using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaveGame : MonoBehaviour
{
    public void Leave()
    {
        PhotonNetwork.LeaveRoom();
        PhotonNetwork.LeaveLobby();
        
        SceneManager.LoadScene(sceneBuildIndex: 0);
        PhotonNetwork.Disconnect();
    } 
}
