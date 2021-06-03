using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectModButton : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Text label;

    [SerializeField]
    private string modSlotName;

    private string activeMod;

    // Start is called before the first frame update
    void Start()
    {
        activeMod = PlayerPrefs.GetString(modSlotName);
        label.text = activeMod;
    }

    public void ActivateMod()
    {
        if(activeMod == "Bombe")
        {
            Debug.Log("Bombe");
            player.GetComponent<BombThrow>().ThrowBomb();
        }
        else if(activeMod == "Mine")
        {
            Debug.Log("Mine");
            player.GetComponent<MineDrop>().DropMine();
        }
        else if (activeMod == "Elektro")
        {
            Debug.Log("Elektro");
            player.GetComponent<Electroshock>().ActivateElectroshock();
        }
        else if (activeMod == "Schild")
        {
            Debug.Log("Schild");
            player.GetComponent<Shield>().ActivateShield();
        }
        else if (activeMod == "Nitro")
        {
            Debug.Log("Nitro");
            player.GetComponent<Nitro>().ActivateNitro();
        }
        else if (activeMod == "Pfütze")
        {
            Debug.Log("Pfütze");
            player.GetComponent<PfuetzeDrop>().DropPfuetze();
        }
    }
}
