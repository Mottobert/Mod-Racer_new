using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public InputController inputController;

    [SerializeField]
    private PhotonView PV;

    public int energy;
    [SerializeField]
    private Text energyLabel;
    [SerializeField]
    private Text playerLostLabel;
    private bool playerLostBall = false;
    [SerializeField]
    private Text playerNameLabel;
    public string playerName;

    public GameObject ball;

    public GameObject ballSpawn;

    private CarController carController;

    public int carConfigurationIndex;

    public GameObject allCars;

    public GameObject wheelFrontLeft;
    public GameObject wheelFrontRight;
    public GameObject wheelBackLeft;
    public GameObject wheelBackRight;

    public Transform wheelPositionFrontNarrow;
    public Transform wheelPositionBackNarrow;
    public Transform wheelPositionFrontWide;
    public Transform wheelPositionBackWide;

    void Start()
    {
        //Debug.Log(PV.IsMine);
        //Debug.Log(PV.Owner);
        if (PV.IsMine)
        {
            playerName = PlayerPrefs.GetString("PlayerName");
            UpdateEnergyLabel();
            UpdatePlayerNameLabel();

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

    public void PlayerLostBall()
    {
        playerLostLabel.text = "Du hast deinen Ball verloren!";
        playerLostBall = true;
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
}
