using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class PlayerNameTag : MonoBehaviourPun
{
    [SerializeField]
    public Text nameText;

    [SerializeField]
    public Text lifesText;

    void Start()
    {
        if (photonView.IsMine)
        {
            return;
        }

        SetName();

        UpdateLifesLabel(3);
    }

    private void SetName()
    {
        nameText.text = photonView.Owner.NickName;
    }

    public void UpdateLifesLabel(int playerLifes)
    {
        lifesText.text = "" + playerLifes;
    }
}
