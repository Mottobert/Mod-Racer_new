using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DragDrop : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    [SerializeField]
    private Canvas canvas;

    [SerializeField]
    public RectTransform parent;

    public string modText;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    public bool droppedInModSlot;

    private void Awake()
    {
        modText = GetComponentInChildren<Text>().text;
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("OnBeginDrag");
        droppedInModSlot = false;
        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        SetAllOthersInactive();
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Debug.Log("OnEndDrag");
        if (!droppedInModSlot)
        {
            //Debug.Log("Test");
            rectTransform.anchoredPosition = parent.anchoredPosition;
        }
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
        SetAllOthersActive();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        //Debug.Log("OnPointerDown");
    }

    public void SetAllOthersInactive()
    {
        GameObject[] mods = GameObject.FindGameObjectsWithTag("Mod");

        //Debug.Log(mods.Length);

        foreach(GameObject m in mods)
        {
            if(m != this)
            {
                m.GetComponent<CanvasGroup>().blocksRaycasts = false;
            }
        }
    }

    public void SetAllOthersActive()
    {
        GameObject[] mods = GameObject.FindGameObjectsWithTag("Mod");

        //Debug.Log(mods.Length);

        foreach (GameObject m in mods)
        {
            if (m != this)
            {
                m.GetComponent<CanvasGroup>().blocksRaycasts = true;
            }
        }
    }
}
