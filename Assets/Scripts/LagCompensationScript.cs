using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class LagCompensationScript : MonoBehaviour, IPunObservable
{
    private Vector3 networkPosition;
    private Quaternion networkRotation;
    [SerializeField]
    private Rigidbody myRigidbody;

    [SerializeField]
    private PhotonView photonView;

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //Debug.Log("Sending");
        if (stream.IsWriting)
        {
            stream.SendNext(myRigidbody.position);
            stream.SendNext(myRigidbody.rotation);
            stream.SendNext(myRigidbody.velocity);
        }
        else
        {
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();
            myRigidbody.velocity = (Vector3)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.timestamp));
            networkPosition += (this.myRigidbody.velocity * lag);
        }
    }

    public void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            Vector3 newPosition = Vector3.MoveTowards(myRigidbody.position, networkPosition, Time.fixedDeltaTime);
            Quaternion newRotation = Quaternion.RotateTowards(myRigidbody.rotation, networkRotation, Time.fixedDeltaTime * 100.0f);

            Vector3 lagDistance = myRigidbody.position - networkPosition;

            if (lagDistance.magnitude > 3f)
            {
                newPosition = networkPosition;
                lagDistance = Vector3.zero;
            }
            myRigidbody.position = newPosition;
            myRigidbody.rotation = newRotation;
        }
    }
}
