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

    private bool active = true;

    // Start is called before the first frame update
    void Start()
    {
        activeMod = PlayerPrefs.GetString(modSlotName);
        label.text = activeMod;
    }

    public void ActivateMod()
    {
        if(activeMod == "Bombe" && active)
        {
            //Debug.Log("Bombe");
            player.GetComponent<BombThrow>().ThrowBomb();
        }
        else if(activeMod == "Mine" && active)
        {
            //Debug.Log("Mine");
            player.GetComponent<MineDrop>().DropMine();
        }
        else if (activeMod == "Elektro" && active)
        {
            //Debug.Log("Elektro");
            player.GetComponent<Electroshock>().ActivateElectroshock();
        }
        else if (activeMod == "Schild" && active)
        {
            //Debug.Log("Schild");
            player.GetComponent<Shield>().ActivateShield();
        }
        else if (activeMod == "Nitro" && active)
        {
            //Debug.Log("Nitro");
            player.GetComponent<Nitro>().ActivateNitro();
        }
        else if (activeMod == "Pfütze" && active)
        {
            //Debug.Log("Pfütze");
            player.GetComponent<PfuetzeDrop>().DropPfuetze();
        }
    }

    public void DisableMod()
    {
        active = false;
    }

    public void EnableMod()
    {
        active = true;
    }
}
