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
        if(playerLifes > 1)
        {
            playerLostLabel.text = "Du hast deinen Ball verloren!";
            playerLostBall = true;

            playerLifes--;
            UpdatePlayerLifesLabel();

            ResetTimer();

            //Invoke("ResetPlayerPositionWithBall", 3f);
        } else if(playerLifes == 1)
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
        inputController.playerCamera.GetComponentInParent<PhotonPlayer>().ResetPlayer(playerLostBall);
        carController.ResetVelocity();
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    public void ResetPlayerPositionWithBall()
    {
        bool restoreBall = false; 
        inputController.playerCamera.GetComponentInParent<PhotonPlayer>().ResetPlayer(restoreBall);
        carController.ResetVelocity();
        ball.GetComponent<Rigidbody>().velocity = Vector3.zero;
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
            AllowPlayerInputs();
        }
    }

    private void DenyPlayerInputs()
    {
        inputController.allowInputs = false;
        inputController.ResetDirection();
        inputController.ResetSteerInput();
    }

    private void AllowPlayerInputs()
    {
        inputController.allowInputs = true;
    }

    private void DisablePlayer()
    {
        DenyPlayerInputs();
        carController.ResetVelocity();
        inputController.ResetDirection();
        inputController.ResetSteerInput();
    }
}
