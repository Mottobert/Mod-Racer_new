using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public InputController inputController;

    [SerializeField]
    public PhotonView PV;

    public int energy;
    [SerializeField]
    private Text energyLabel;
    [SerializeField]
    private Text playerLostLabel;
    private bool playerLostBall = false;
    [SerializeField]
    private Text playerNameLabel;
    public string playerName;

    [SerializeField]
    private Text resetTimerLabel;
    private int resetTimer = 4;

    [SerializeField]
    private Text playerLifesLabel;
    public int playerLifes = 3;

    public GameObject ball;

    public GameObject ballSpawn;
    [SerializeField]
    private GameObject schale;

    private CarController carController;

    public int carConfigurationIndex;

    public GameObject allCars;

    public UITargetController uiTargetController;

    private bool resetButtonActive = true;

    [SerializeField]
    private GameObject buttonMod1;
    [SerializeField]
    private GameObject buttonMod2;

    public bool activePlayer = true;

    [SerializeField]
    private GameObject playCanvas;
    [SerializeField]
    private GameObject spectatorCanvas;

    [SerializeField]
    private GameObject spawnCollider;

    private Collider[] colliders;
    private List<Collider> nonTriggerColliders = new List<Collider>();

    public bool invisible = true;

    void Start()
    {
        //Debug.Log(PV.IsMine);
        //Debug.Log(PV.Owner);
        if (PV.IsMine)
        {
            //Debug.Log("Start invisible " + invisible);
            playerName = PlayerPrefs.GetString("PlayerName");
            UpdateEnergyLabel();
            UpdatePlayerNameLabel();
            UpdatePlayerLifesLabel();

            carController = GetComponent<CarController>();
        }


        // Collect all Colliders of Player
        colliders = gameObject.GetComponentsInChildren<Collider>();

        foreach (Collider c in colliders)
        {
            //if (!c.isTrigger)
            //{
            //    nonTriggerColliders.Add(c);
            //    Debug.Log(c);
            //}

            if(c.name != "SlipperyController" && c.name != "SpawnCollider" && c.name != "BallContainerTrigger")
            {
                nonTriggerColliders.Add(c);
                //Debug.Log(c);
            }
        }

        if (PV.IsMine)
        {
            PV.RPC("SetPlayerInvisibleForAll", RpcTarget.AllBufferedViaServer, PV.ViewID);
            SetPlayerInvisible();
        }

        if (!PV.IsMine)
        {
            //PV.RPC("SetPlayerVisibleForAll", RpcTarget.All, PV.ViewID);
            Debug.Log("Set Other Player Visible");
            SetPlayerVisible();
        }


        //Debug.Log("colliders " + colliders.Length);



        //if (PhotonNetwork.IsMasterClient)
        //{
        //    Debug.Log(PV.Owner);
        //    GameObject.Find("EnergyBallSpawnerPoints").GetComponent<EnergyBallSpawner>().PlaceRandomEnergyBalls(3);
        //}

        if (PV.IsMine)
        {
            //Debug.Log("After Start invisible " + invisible);
        }
    }

    

    [PunRPC]
    public void SetPlayerInvisibleForAll(int viewID)
    {
        PhotonView.Find(viewID).gameObject.GetComponent<Player>().SetPlayerInvisible();
    }

    [PunRPC]
    public void SetPlayerVisibleForAll(int viewID)
    {
        Debug.Log("Visible for all " + viewID);
        PhotonView.Find(viewID).gameObject.GetComponent<Player>().SetPlayerVisible();
    }

    public void SetPlayerInvisible()
    {
        foreach (Collider c in nonTriggerColliders)
        {
            if (c.name != "WheelColliderLeftFront" && c.name != "WheelColliderRightFront" && c.name != "WheelColliderLeftBack" && c.name != "WheelColliderRightBack" && c.name != "SpawnCube")
            {
                c.isTrigger = true;
                //Debug.Log(c);
            }
        }
        
        ChangeAlpha(gameObject.GetComponent<MeshRenderer>().material, 0.5f);
        ChangeAlpha(schale.GetComponent<MeshRenderer>().material, 0.5f);

        invisible = true;
        invisibleCounter = 0;

        if (PV.IsMine)
        {
            schale.GetComponentInChildren<BallContainer>().active = false;
        }

        //Debug.Log("Non Trigger Colliders " + nonTriggerColliders.Count);
    }

    public void SetPlayerVisible()
    {
        foreach (Collider c in nonTriggerColliders)
        {
            if (c.name != "WheelColliderLeftFront" && c.name != "WheelColliderRightFront" && c.name != "WheelColliderLeftBack" && c.name != "WheelColliderRightBack" && c.name != "SpawnCube")
            {
                c.isTrigger = false;
                //if (!PV.IsMine)
                //{
                //    Debug.Log(c.gameObject.GetComponentInChildren<InputController>().PV.Owner);
                //}
            }
        }

        ChangeAlpha(gameObject.GetComponent<MeshRenderer>().material, 1f);
        ChangeAlpha(schale.GetComponent<MeshRenderer>().material, 1f);
        invisible = false;

        if (PV.IsMine)
        {
            schale.GetComponentInChildren<BallContainer>().active = true;
        }
    }

    public void ChangeAlpha(Material mat, float alphaVal)
    {
        Color oldColor = gameObject.GetComponent<MeshRenderer>().material.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaVal);

        mat.SetColor("_Color", newColor);
    }

    int invisibleCounter = 0;

    void Update()
    {
        //if (PV.IsMine)
        //{
        //    Debug.Log("ActivePlayer " + activePlayer);
        //
        //    Debug.Log("PV " + PV.IsMine);
        //
        //    Debug.Log("isBlocked " + !spawnCollider.GetComponent<SpawnCollisionChecker>().isBlocked);
        //
        //    Debug.Log("Invisible " + invisible);
        //
        //    Debug.Log("Ball " + ball);
        //
        //    Debug.Log("Counter " + invisibleCounter);
        //}


        //if (!PV.IsMine)
        //{
        //    Debug.Log("Blocked " + spawnCollider.GetComponent<SpawnCollisionChecker>().isBlocked);
        //}



        if (PV.IsMine && !spawnCollider.GetComponent<SpawnCollisionChecker>().isBlocked && invisible && invisibleCounter > 20 && activePlayer)
        {
            Debug.Log("Not Intersecting with other Player");

            if (ball != null)
            {
                ball.transform.position = ballSpawn.transform.position;
            }
            else
            {
                Debug.Log("SpawnBall");
                ball = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Ball"), ballSpawn.transform.position, Quaternion.identity);
            }

            PV.RPC("SetPlayerVisibleForAll", RpcTarget.AllBufferedViaServer, PV.ViewID);
            SetPlayerVisible();
        }

        //if (!PV.IsMine && !invisible)
        //{
        //    Debug.Log("Set Other Player Visible");
        //    //PV.RPC("SetPlayerVisibleForAll", RpcTarget.All, PV.ViewID);
        //    SetPlayerVisible();
        //}

        if (PV.IsMine)
        {
            carController.Steer = inputController.SteerInput;
            carController.Throttle = inputController.ThrottleInput;
        }

        if (invisibleCounter < 21)
        {
            invisibleCounter++;
        }
    }

    public void IncreaseEnergy()
    {
        energy++;
        UpdateEnergyLabel();
    }

    public void UpdateEnergyLabel()
    {
        energyLabel.text = "Energie: " + energy;
    }

    public void UpdatePlayerNameLabel()
    {
        playerNameLabel.text = playerName;
    }

    public void UpdatePlayerLifesLabel()
    {
        playerLifesLabel.text = "Leben: " + playerLifes;
    }

    public void UpdatePlayerResetTimerLabel()
    {
        resetTimerLabel.text = "" + resetTimer;
    }

    [PunRPC]
    public void UpdatePlayerLifesForAll(int viewID, int playerLifes)
    {
        PhotonView.Find(viewID).gameObject.GetComponent<Player>().SetPlayerLifes(playerLifes);
    }

    private void SetPlayerLifes(int playerLifes)
    {
        if (!PV.IsMine)
        {
            this.playerLifes = playerLifes;
            //gameObject.GetComponent<PlayerNameTag>().UpdateLifesLabel(playerLifes);
        }
    }

    public void PlayerLostBall()
    {
        if (playerLifes > 1)
        {
            //playerLostLabel.text = "Du hast deinen Ball verloren!";
            playerLostBall = true;

            playerLifes--;
            //PV.RPC("UpdatePlayerLifesForAll", RpcTarget.AllBufferedViaServer, PV.ViewID, playerLifes);
            //UpdatePlayerLifesForAll(PV.ViewID, playerLifes);
            UpdatePlayerLifesLabel();

            ResetTimer();

            //Invoke("ResetPlayerPositionWithBall", 3f);
        } else if (playerLifes == 1)
        {
            playerLifes--;
            //PV.RPC("UpdatePlayerLifesForAll", RpcTarget.AllBufferedViaServer, PV.ViewID, playerLifes);
            UpdatePlayerLifesLabel();
            playerLostLabel.text = "Du hast alle deine Leben verloren!";
            DisablePlayer();
            SetPlayerInvisible();
        }
    }

    private GameObject FindChild(Transform parent, int index)
    {
        for (int i = 0; i < parent.childCount; i++)
        {
            GameObject g = parent.GetChild(i).gameObject;
            if (i == index)
                return g;
        }
        return null;
    }

    public void ResetPlayerPosition()
    { 
        if(resetButtonActive){
            if (PV.IsMine)
            {
                PV.RPC("SetPlayerInvisibleForAll", RpcTarget.AllBufferedViaServer, PV.ViewID);
                SetPlayerInvisible();
            }
            Invoke("ResetPlayerPositionDelay", 0.1f);
        }
    }

    private void ResetPlayerPositionDelay()
    {
        inputController.playerCamera.GetComponentInParent<PhotonPlayer>().ResetPlayer(playerLostBall);
        carController.ResetVelocity();
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void ResetPlayerPositionWithBall()
    {
        bool restoreBall = false;
        if (inputController.playerCamera)
        {
            inputController.playerCamera.GetComponentInParent<PhotonPlayer>().ResetPlayer(restoreBall);
            carController.ResetVelocity();
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
    }

    private void ResetTimer()
    {
        if(resetTimer > 1)
        {
            DenyPlayerInputs();
            resetTimer--;
            UpdatePlayerResetTimerLabel();

            Invoke("ResetTimer", 1f);
        }
        else if(resetTimer == 1)
        {
            if (PV.IsMine)
            {
                PV.RPC("SetPlayerInvisibleForAll", RpcTarget.AllBufferedViaServer, PV.ViewID);
                //SetPlayerInvisible();
            }
            Invoke("ResetPlayerPositionWithBall", 0.2f);
            //ResetPlayerPositionWithBall();
            resetTimer = 4;
            resetTimerLabel.text = "";

            playerLostLabel.text = "";
            playerLostBall = false;
            Invoke("AllowPlayerInputs", 0.1f);
        }
    }

    private void DenyPlayerInputs()
    {
        inputController.allowInputs = false;
        Invoke("ResetPlayerInputs", 0.2f);
        resetButtonActive = false;
        buttonMod1.GetComponent<SelectModButton>().DisableMod();
        buttonMod2.GetComponent<SelectModButton>().DisableMod();
    }

    private void ResetPlayerInputs()
    {
        inputController.ResetDirection();
        inputController.ResetSteerInput();
    }

    private void AllowPlayerInputs()
    {
        inputController.allowInputs = true;
        resetButtonActive = true;
        buttonMod1.GetComponent<SelectModButton>().EnableMod();
        buttonMod2.GetComponent<SelectModButton>().EnableMod();
    }

    private void DisablePlayer()
    {
        DenyPlayerInputs();
        PV.RPC("ChangeLayerToInvisibleForAll", RpcTarget.AllBufferedViaServer, PV.ViewID);
        //gameObject.layer = LayerMask.NameToLayer("InvisibleCar");
        carController.ResetVelocity();

        activePlayer = false;
        playCanvas.SetActive(false);
        playCanvas.GetComponent<CanvasGroup>().interactable = false;
        playCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        playCanvas.GetComponent<CanvasGroup>().alpha = 0;
        spectatorCanvas.SetActive(true);
        spectatorCanvas.GetComponent<SpectatorMode>().GetAllPlayers();
        spectatorCanvas.GetComponent<CanvasGroup>().interactable = true;
        spectatorCanvas.GetComponent<CanvasGroup>().alpha = 1;
    }

    public void ChangeLayerToInvisible()
    {
        gameObject.layer = LayerMask.NameToLayer("InvisibleCar");
    }

    [PunRPC]
    public void ChangeLayerToInvisibleForAll(int viewID)
    {
        PhotonView.Find(viewID).gameObject.GetComponent<Player>().ChangeLayerToInvisible();
    }
}
