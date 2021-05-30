using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ChangeScene : MonoBehaviour
{
    [SerializeField]
    private int sceneIndex;
    [SerializeField]
    private bool disconnectOnSceneChange;

    public void ChangeSceneOnClick()
    {
        if (PhotonNetwork.IsConnected && disconnectOnSceneChange)
        {
            PhotonNetwork.Disconnect();
        }
        SceneManager.LoadScene(sceneBuildIndex: sceneIndex);
    }
}
