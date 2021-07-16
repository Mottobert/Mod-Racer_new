using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetSlipperyOnDelete : MonoBehaviour
{
    private List<GameObject> playerList = new List<GameObject>();

    private void OnDestroy()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        for(int i = 0; i < players.Length; i++)
        {
            if (players[i].name != "SpawnCube")
            {
                playerList.Add(players[i]);
            }
        }

        foreach(GameObject p in playerList)
        {
            Debug.Log("Reset Slippery");
            p.GetComponentInChildren<SlipperyController>().ResetWheelsSlipperyOnExit();
        }
    }
}
