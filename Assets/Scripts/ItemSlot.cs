using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemSlot : MonoBehaviour, IDropHandler
{
    public string modSlotName;

    private bool blocked = false;

    void Awake()
    {
        Debug.Log("PlayerPrefs bevor Awake " + modSlotName + " " + PlayerPrefs.GetString(modSlotName));
        if (PlayerPrefs.GetString(modSlotName) == null || PlayerPrefs.GetString(modSlotName) == "") {
            if (modSlotName == "Mod1")
            {
                PlayerPrefs.SetString(modSlotName, "Bombe");
            }
            else if (modSlotName == "Mod2")
            {
                PlayerPrefs.SetString(modSlotName, "Schild");
            }
        }
    }

    void Start()
    {
        Debug.Log("PlayerPrefs nach Awake " + modSlotName + " " + PlayerPrefs.GetString(modSlotName));
        Debug.Log(PlayerPrefs.GetString(modSlotName));
        if (PlayerPrefs.GetString(modSlotName) != null && PlayerPrefs.GetString(modSlotName) != "")
        {
            blocked = true;
            GameObject.Find(PlayerPrefs.GetString(modSlotName)).GetComponent<RectTransform>().anchoredPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
        }
    }

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("OnDrop");
        //Debug.Log(eventData.pointerDrag);
        if (eventData.pointerDrag != null)
        {
            if (blocked)
            {
                GameObject.Find(PlayerPrefs.GetString(modSlotName)).GetComponent<RectTransform>().anchoredPosition = GameObject.Find(PlayerPrefs.GetString(modSlotName)).GetComponent<DragDrop>().parent.GetComponent<RectTransform>().anchoredPosition;
            }
            blocked = true;
            eventData.pointerDrag.GetComponent<DragDrop>().droppedInModSlot = true;
            //Debug.Log("anchoredPosition: " + gameObject.GetComponent<RectTransform>().anchoredPosition);
            eventData.pointerDrag.GetComponent<RectTransform>().anchoredPosition = gameObject.GetComponent<RectTransform>().anchoredPosition;
            PlayerPrefs.SetString(modSlotName, eventData.pointerDrag.GetComponent<DragDrop>().modText);

            Debug.Log(PlayerPrefs.GetString(modSlotName));
        }
    }
}
