using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    private PhotonView PV;
    private GameObject myAvatar;
    private GameObject myBall;
    [SerializeField]
    private GameObject avatarCamera;

    private GameObject playerSpawnPositions;

    // Start is called before the first frame update
    void Start()
    {
        playerSpawnPositions = GameObject.FindGameObjectWithTag("PlayerSpawnPoints");

        PV = GetComponent<PhotonView>();

        if (PV.IsMine)
        {
            Invoke("SpawnPlayer", 0.1f);
        }
    }

    private void SpawnPlayer()
    {
        Transform spawnPosition = PickRandomSpawn(playerSpawnPositions);
        SpawnAvatar(spawnPosition);
        SpawnBall();
        ConnectCameraToAvatar();
        ConnectBallToAvatar();
    }

    private void SpawnAvatar(Transform playerSpawn)
    {
        myAvatar = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerAvatar0" + PlayerPrefs.GetInt("SelectedCarIndex")), playerSpawn.position, playerSpawn.rotation);
    }

    private void SpawnBall()
    {
        myBall = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Ball"), myAvatar.GetComponent<Player>().ballSpawn.transform.position, Quaternion.identity);
    }

    private void ConnectCameraToAvatar()
    {
        myAvatar.GetComponentInChildren<InputController>().playerCamera = avatarCamera.GetComponentInChildren<Camera>();
        avatarCamera.GetComponent<CameraFollow>().Target = myAvatar.transform;

        avatarCamera.GetComponent<Camera>().gameObject.SetActive(false);
    }

    private void ConnectBallToAvatar()
    {
        myAvatar.GetComponent<Player>().ball = myBall;
    }

    private Transform PickRandomSpawn(GameObject playerSpawns)
    {
        for (int i = 0; i < playerSpawns.transform.childCount - 1; i++)
        {
            if (!playerSpawns.transform.GetChild(i).GetComponent<SpawnPoint>().isBlocked)
            {
                //Debug.Log(playerSpawns.transform.GetChild(i).GetComponent<SpawnPoint>().isBlocked);
                return playerSpawns.transform.GetChild(i).transform;
            }
        }

        return null;
    }

    public void ResetPlayer(bool lostBall)
    {
        if (PV.IsMine)
        {
            Transform resetTransform = PickRandomSpawn(playerSpawnPositions);
            myAvatar.transform.position = resetTransform.position;
            if (!lostBall)
            {
                myBall.transform.position = myAvatar.GetComponent<Player>().ballSpawn.transform.position;
            }
            myAvatar.transform.rotation = resetTransform.rotation;
        }
    }

    //private Transform FindChild(Transform parent, string name)
    //{
    //    for (int i = 0; i < parent.childCount; i++)
    //    {
    //        Transform t = parent.GetChild(i);
    //        if (t.name == name)
    //            return t;
    //    }
    //    return null;
    //}
}
