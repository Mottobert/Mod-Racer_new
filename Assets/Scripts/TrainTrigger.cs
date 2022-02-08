using System.Collections;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using Photon.Pun;
using UnityEngine;

public class TrainTrigger : MonoBehaviour
{
    [SerializeField]
    private GameObject train;

    [SerializeField]
    private MMFeedbacks trainFeedback;

    // Start is called before the first frame update
    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            SetRandomTrainTimer();
        }
    }

    [ContextMenu("StartTrain")]
    private void TriggerTrain()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            StartTrain();
        }
    }

    private void SetRandomTrainTimer()
    {
        float randomDelay = Random.Range(100f, 160f);

        Invoke("TriggerTrain", randomDelay);
    }

    private void StartTrain()
    {
        gameObject.GetComponent<PhotonView>().RPC("StartTrainAnimationForAll", RpcTarget.AllBufferedViaServer);
    }

    [PunRPC]
    public void StartTrainAnimationForAll()
    {
        StartTrainAnimation();
    }

    private void StartTrainAnimation()
    {
        train.GetComponent<Animator>().SetTrigger("activate");
        trainFeedback.PlayFeedbacks();
    }
}
