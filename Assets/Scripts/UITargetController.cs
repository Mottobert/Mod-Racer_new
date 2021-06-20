using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public class UITargetController : MonoBehaviour
{
    public Canvas canvas;

    public List<TargetIndicator> targetIndicators = new List<TargetIndicator>();

    public List<GameObject> targetList = new List<GameObject>();

    public Camera mainCamera;

    public GameObject TargetIndicatorPrefab;

    public InputController inputController;

    public GameObject player;

    private void AddPlayerTargets()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Player");

        if (targets != null)
        {
            foreach (GameObject t in targets)
            {
                //Debug.Log("IsMine " + t.GetComponent<PhotonView>());
                if(t.GetComponent<PhotonView>() != null)
                {
                    if (t.GetComponent<PhotonView>().IsMine && t != player)
                    {
                        //Debug.Log(t.GetComponent<Player>().inputController.playerCamera.isActiveAndEnabled);
                        Camera mainCameraPlayer = t.GetComponent<Player>().inputController.playerCamera;
                        t.GetComponent<Player>().uiTargetController.AddTargetIndicator(player, mainCameraPlayer);
                    }
                }
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Invoke("AddPlayerTargets", 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(targetIndicators.Count > 0)
        {
            for(int i = 0; i < targetIndicators.Count; i++)
            {
                if(targetIndicators[i] != null)
                {
                    targetIndicators[i].UpdateTargetIndicator();
                }
            }
        }
    }

    public void AddTargetIndicator(GameObject target, Camera playerCamera)
    {
        TargetIndicator indicator = GameObject.Instantiate(TargetIndicatorPrefab, canvas.transform).GetComponent<TargetIndicator>();
        indicator.InitialiseTargetIndicator(target, playerCamera, canvas);
        targetIndicators.Add(indicator);

        Debug.Log("Test " + indicator);
    }
}
