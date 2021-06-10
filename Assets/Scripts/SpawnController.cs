using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    public static SpawnController instance;

    public Transform[] spawnPoints;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }
}
