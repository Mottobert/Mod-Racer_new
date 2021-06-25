using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class TrainTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject train;

    // Start is called before the first frame update
    void Start()
    {
        Invoke("TriggerTrain", 30f);
    }

    private void TriggerTrain()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartTrainDelayed();
        }

        float randomDelay = Random.Range(80f, 120f);
        Debug.Log(randomDelay);

        Invoke("TriggerTrain", randomDelay);
        //TriggerTrain(randomDelay);
    }

    private void StartTrainDelayed()
    {
        gameObject.GetComponent<PhotonView>().RPC("StartTrainForAll", RpcTarget.All);
    }

    [PunRPC]
    public void StartTrainForAll()
    {
        StartTrain();
    }

    private void StartTrain()
    {
        //train.GetComponent<Animator>().StopPlayback();
        train.GetComponent<Animator>().Play("Train_Crossing", -1, 0f);
    }
}
