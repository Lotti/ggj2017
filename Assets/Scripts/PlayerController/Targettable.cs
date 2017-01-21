using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Targettable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("Gaze Start Focus");

    }

    public void OnPointerExit(PointerEventData data)
    {

        Debug.Log("Gaze End Focus");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Gaze Click ");

    }

    public void OnPointerStay(PointerEventData eventData)
    {
        Debug.Log("Gaze Stay Focus");
    }

}
