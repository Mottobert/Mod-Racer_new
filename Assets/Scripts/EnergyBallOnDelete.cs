using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Photon.Pun;

public class EnergyBallOnDelete : MonoBehaviour
{
    public GameObject spawner; //Das Transform-GameObject
    public GameObject spawners; //Die Gruppe von Spawners

    private void OnDestroy()
    {
        if (Application.isPlaying && spawner != null && spawners != null)
        {
            spawners.GetComponent<EnergyBallSpawner>().PlaceRandomEnergyBalls(1);
            spawners.GetComponent<EnergyBallSpawner>().AddToSpawners(spawner);
        }
    }
}
