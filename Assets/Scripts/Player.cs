using System.Collections;
using System.Collections.Generic;
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

    private CarController carController;

    public int carConfigurationIndex;

    public GameObject allCars;

    public UITargetController uiTargetController;

    private bool resetButtonActive = true;

    [SerializeField]
    private GameObject buttonMod1;
    [SerializeField]
    private GameObject buttonMod2;

    void Start()
    {
        //Debug.Log(PV.IsMine);
        //Debug.Log(PV.Owner);
        if (PV.IsMine)
        {
            playerName = PlayerPrefs.GetString("PlayerName");
            UpdateEnergyLabel();
            UpdatePlayerNameLabel();
            UpdatePlayerLifesLabel();

            carController = GetComponent<CarController>();
        }

        //if (PhotonNetwork.IsMasterClient)
        //{
        //    Debug.Log(PV.Owner);
        //    GameObject.Find("EnergyBallSpawnerPoints").GetComponent<EnergyBallSpawner>().PlaceRandomEnergyBalls(3);
        //}
    }

    void Update()
    {
        if (PV.IsMine)
        {
            carController.Steer = inputController.SteerInput;
            carController.Throttle = inputController.ThrottleInput;
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

    public void PlayerLostBall()
    {
        if (playerLifes > 1)
        {
            playerLostLabel.text = "Du hast deinen Ball verloren!";
            playerLostBall = true;

            playerLifes--;
            UpdatePlayerLifesLabel();

            ResetTimer();

            //Invoke("ResetPlayerPositionWithBall", 3f);
        } else if (playerLifes == 1)
        {
            playerLifes--;
            UpdatePlayerLifesLabel();
            playerLostLabel.text = "Du hast alle deine Leben verloren!";
            DisablePlayer();
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
            inputController.playerCamera.GetComponentInParent<PhotonPlayer>().ResetPlayer(playerLostBall);
            carController.ResetVelocity();
            ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
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
            ResetPlayerPositionWithBall();
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
        carController.ResetVelocity();
        inputController.ResetDirection();
        inputController.ResetSteerInput();
    }
}
