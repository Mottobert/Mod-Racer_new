﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    public bool active = false;

    [SerializeField]
    private GameObject shieldObject; 
    [SerializeField]
    private float duration = 3f;
    [SerializeField]
    private int cost = 5;

    public void ActivateShield()
    {
        if (this.gameObject.GetComponent<Player>().energy >= cost)
        {
            this.gameObject.GetComponent<Player>().energy = this.gameObject.GetComponent<Player>().energy - cost;
            this.gameObject.GetComponent<Player>().UpdateEnergyLabel();
            active = true;
            shieldObject.SetActive(true);

            Invoke("DeactivateShield", duration);
        }
    }

    private void DeactivateShield()
    {
        active = false;
        shieldObject.SetActive(false);
    }
}
