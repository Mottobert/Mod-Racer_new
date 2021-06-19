using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class PhotonPlayer : MonoBehaviour
{
    public PhotonView PV;
    private Photon.Realtime.Player[] allPlayers;
    private int myNumberInRoom = 0;

    private GameObject myAvatar;
    private GameObject myBall;
    [SerializeField]
    private GameObject avatarCamera;

    private GameObject playerSpawnPositions;
    private Transform playerSpawnPosition;

    private void Awake()
    {
        if (PlayerPrefs.GetString("Mod1") == null || PlayerPrefs.GetString("Mod1") == "")
        {
            PlayerPrefs.SetString("Mod1", "Bombe");
        }

        if (PlayerPrefs.GetString("Mod2") == null || PlayerPrefs.GetString("Mod2") == "")
        {
            PlayerPrefs.SetString("Mod2", "Schild");
        }

        //PlayerPrefs.DeleteAll();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerSpawnPositions = GameObject.FindGameObjectWithTag("PlayerSpawnPoints");

        PV = GetComponent<PhotonView>();

        allPlayers = PhotonNetwork.PlayerList;

        Debug.Log(PV.ViewID);

        string viewIDString = "" + PV.ViewID;
        string firstDigitOfViewID = viewIDString.Substring(0, 1);
        int firstDigitOfViewIDInt = int.Parse(firstDigitOfViewID) - 1;

        myNumberInRoom = firstDigitOfViewIDInt;

        Debug.Log(firstDigitOfViewIDInt);

        //foreach (Photon.Realtime.Player p in allPlayers)
        //{
        //    if(p != PhotonNetwork.LocalPlayer)
        //    {
        //        myNumberInRoom++;
        //        Debug.Log(myNumberInRoom);
        //        Debug.Log(p);
        //    }
        //}

        if (PV.IsMine)
        {
            //SpawnController.instance.spawnPoints[myNumberInRoom];

            //SpawnPlayer(spawnPosition);
            Invoke("SpawnPlayer", 0.3f);
        }

        //if (PV.IsMine)
        //{
        //    Invoke("SpawnPlayer", 0.3f); // Changed
        //}
    }

    private Transform GetSpawnPosition()
    {
        if (PlayerPrefs.GetInt("SpawnMode") == 0)
        {
            Debug.Log("Custom Matchmaking Spawn");
            return SpawnController.instance.spawnPoints[myNumberInRoom];
        }
        else
        {
            Debug.Log("Quick Start Spawn");
            return PickRandomSpawn(playerSpawnPositions);
        }
        //Transform spawnPosition = PickRandomSpawn(playerSpawnPositions);
    }

    private void SpawnPlayer()
    {
        playerSpawnPosition = GetSpawnPosition();
        SpawnAvatar(playerSpawnPosition);
        //SpawnBall();
        ConnectCameraToAvatar();
        //ConnectBallToAvatar();
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
                //myAvatar.GetComponent<Player>().ball.transform.position = myAvatar.GetComponent<Player>().ballSpawn.transform.position;
            }
            myAvatar.transform.rotation = resetTransform.rotation;
        }
    }
}