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

    [SerializeField]
    private float lagDistanceTreshold = 2f;
    [SerializeField]
    private float lagRotationTreshold = 30f;

    public float smoothPos = 5.0f;
    public float smoothRot = 5.0f;

    private void Awake()
    {
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 10;
    }

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

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            networkPosition += (this.myRigidbody.velocity * lag);
        }
    }

    public void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            Vector3 newPosition = Vector3.Lerp(myRigidbody.position, networkPosition, smoothPos * Time.fixedDeltaTime);
            Quaternion newRotation = Quaternion.Lerp(myRigidbody.rotation, networkRotation, smoothRot * Time.fixedDeltaTime);

            Vector3 lagDistance = myRigidbody.position - networkPosition;
            Vector3 lagRotation = myRigidbody.rotation.eulerAngles - networkRotation.eulerAngles;

            if (lagDistance.magnitude > lagDistanceTreshold)
            {
                newPosition = networkPosition;
                lagDistance = Vector3.zero;
            }

            //Debug.Log(Mathf.Abs(lagRotation.y));
            //Debug.Log("Network: " + networkRotation.y);
            //Debug.Log("New: " + newRotation.y);

            if (Mathf.Abs(lagRotation.y) > lagRotationTreshold)
            {
                //Debug.Log("Test");
                newRotation = networkRotation;
                lagRotation = Vector3.zero;
            }

            myRigidbody.position = newPosition;
            myRigidbody.rotation = newRotation;
        }
    }
}
