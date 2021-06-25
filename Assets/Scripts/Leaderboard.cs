using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField]
    private GameObject[] playerLabels;

    private List<GameObject> playerList = new List<GameObject>();

    private GameObject[] players;

    public void ListPlayers()
    {
        playerList.Clear();
        players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < players.Length; i++)
        {
            if (players[i].name != "SpawnCube")
            {
                playerList.Add(players[i]);
            }
        }

        if(playerList.Count == 2)
        {
            playerLabels[1].SetActive(true);
        }
        else if(playerList.Count == 3)
        {
            playerLabels[1].SetActive(true);
            playerLabels[2].SetActive(true);
        }
        else if(playerList.Count == 4)
        {
            playerLabels[1].SetActive(true);
            playerLabels[2].SetActive(true);
            playerLabels[3].SetActive(true);
        }

        for(int i = 0; i < playerList.Count; i++)
        {
            playerLabels[i].transform.GetChild(0).GetComponent<Text>().text = playerList[i].GetComponent<Player>().playerName;
            playerLabels[i].transform.GetChild(1).GetComponent<Text>().text = "" + playerList[i].GetComponent<Player>().playerLifes;
        }
    }
}
