using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameTag : MonoBehaviourPun
{
    [SerializeField]
    private Text nameText;

    void Start()
    {
        if (photonView.IsMine)
        {
            return;
        }

        SetName();
    }

    private void SetName()
    {
        nameText.text = photonView.Owner.NickName;
    }
}
