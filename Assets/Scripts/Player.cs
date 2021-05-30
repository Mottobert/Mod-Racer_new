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

            //FindChild(transform, "InputController").SetActive(true);
            carController = GetComponent<CarController>();

            //gameObject.GetComponent<MeshFilter>().mesh = FindChild(allCars.transform, PlayerPrefs.GetInt("SelectedCarIndex")).GetComponent<MeshFilter>().sharedMesh;
            //
            //if(PlayerPrefs.GetInt("SelectedCarIndex") == 6)
            //{
            //    wheelFrontLeft.transform.position = new Vector3(wheelFrontLeft.transform.position.x, wheelFrontLeft.transform.position.y, wheelPositionFrontNarrow.position.z);
            //    wheelFrontRight.transform.position = new Vector3(wheelFrontRight.transform.position.x, wheelFrontRight.transform.position.y, wheelPositionFrontNarrow.position.z);
            //    wheelBackLeft.transform.position = new Vector3(wheelBackLeft.transform.position.x, wheelBackLeft.transform.position.y, wheelPositionBackNarrow.position.z);
            //    wheelBackRight.transform.position = new Vector3(wheelBackRight.transform.position.x, wheelBackRight.transform.position.y, wheelPositionBackNarrow.position.z);
            //}
            //
            //if (PlayerPrefs.GetInt("SelectedCarIndex") == 2 || PlayerPrefs.GetInt("SelectedCarIndex") == 8)
            //{
            //    wheelFrontLeft.transform.position = new Vector3(wheelFrontLeft.transform.position.x, wheelFrontLeft.transform.position.y, wheelPositionFrontWide.position.z);
            //    wheelFrontRight.transform.position = new Vector3(wheelFrontRight.transform.position.x, wheelFrontRight.transform.position.y, wheelPositionFrontWide.position.z);
            //    wheelBackLeft.transform.position = new Vector3(wheelBackLeft.transform.position.x, wheelBackLeft.transform.position.y, wheelPositionBackWide.position.z);
            //    wheelBackRight.transform.position = new Vector3(wheelBackRight.transform.position.x, wheelBackRight.transform.position.y, wheelPositionBackWide.position.z);
            //}

            //if (PlayerPrefs.GetInt("SelectedCarIndex") == 9)
            //{
            //    wheelFrontLeft.transform.position = new Vector3(wheelFrontLeft.transform.position.x, wheelFrontLeft.transform.position.y, wheelPositionFrontNarrow.position.z);
            //    wheelFrontRight.transform.position = new Vector3(wheelFrontRight.transform.position.x, wheelFrontRight.transform.position.y, wheelPositionFrontNarrow.position.z);
            //    wheelBackLeft.transform.position = new Vector3(wheelBackLeft.transform.position.x, wheelBackLeft.transform.position.y, wheelPositionBackNarrow.position.z);
            //    wheelBackRight.transform.position = new Vector3(wheelBackRight.transform.position.x, wheelBackRight.transform.position.y, wheelPositionBackNarrow.position.z);
            //}
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
