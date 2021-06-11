using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class SpectatorMode : MonoBehaviour
{
    [SerializeField]
    private Text spectatorNameLabel;
    [SerializeField]
    private InputController inputController;

    private string spectatorName;
    private int currentPlayer = 0;
    public List<GameObject> activePlayers = new List<GameObject>();
    

    // Start is called before the first frame update
    void Start()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");

        foreach(GameObject p in players)
        {
            if (p.GetComponent<Player>().activePlayer)
            {
                activePlayers.Add(p);
            }
        }

        SetCurrentPlayerTarget();
    }

    public void UpdateSpectatorNameLabel()
    {
        spectatorNameLabel.text = "Du schaust gerade " + spectatorName + " zu";
    }

    public void NextActivePlayer()
    {
        Debug.Log("Next");
        if(currentPlayer < activePlayers.Count - 1)
        {
            currentPlayer++;
        }
        else
        {
            currentPlayer = 0;
        }

        SetCurrentPlayerTarget();
    }

    public void PreviousActivePlayer()
    {
        Debug.Log("Previous");
        if (currentPlayer > 0)
        {
            currentPlayer--;
        }
        else
        {
            currentPlayer = activePlayers.Count - 1;
        }

        SetCurrentPlayerTarget();
    }

    private void SetCurrentPlayerTarget()
    {
        if(activePlayers.Count != 0)
        {
            inputController.playerCamera.gameObject.GetComponent<CameraFollow>().Target = activePlayers[currentPlayer].transform;
            spectatorName = activePlayers[currentPlayer].GetComponent<PhotonView>().Owner.NickName;
            Debug.Log(spectatorName);
            UpdateSpectatorNameLabel();
        }
    }
}
