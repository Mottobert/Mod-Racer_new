using System.Collections;
using System.Collections.Generic;
using System.IO;
using Photon.Pun;
using UnityEngine;

public class EnergyBallSpawner : MonoBehaviour
{
    public GameObject energyBall;
    private List<GameObject> spawners = new List<GameObject>();

    private PhotonView PV;

    // Start is called before the first frame update
    void Start()
    {
        PV = GetComponent<PhotonView>();

        GameObject[] locations = GameObject.FindGameObjectsWithTag("EnergyBallSpawner");

        foreach(GameObject x in locations)
        {
            spawners.Add(x);
        }

        if (PhotonNetwork.IsMasterClient)
        {
            PlaceRandomEnergyBalls(3);
        }
    }

    public void PlaceRandomEnergyBalls(int number)
    {
        for(int i = 0; i < number; i++)
        {
            int randomIndex = Random.Range(0, spawners.Count);

            PlaceEnergyBall(randomIndex);
            DeleteSpawner(randomIndex);
        }
    }

    private void PlaceEnergyBall(int index)
    {
        GameObject Ball = PhotonNetwork.InstantiateRoomObject(Path.Combine("PhotonPrefabs", "EnergyBall"), spawners[index].transform.position, spawners[index].transform.rotation);

        Ball.GetComponent<EnergyBallOnDelete>().spawner = spawners[index];
        Ball.GetComponent<EnergyBallOnDelete>().spawners = this.gameObject;
    }

    private void DeleteSpawner(int index)
    {
        spawners.RemoveAt(index);
    }

    public void AddToSpawners(GameObject spawner)
    {
        spawners.Add(spawner);
    }
}
