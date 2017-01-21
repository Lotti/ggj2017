using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Targettable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public virtual void StartFocus()
    { }

    public virtual void EndFocus()
    { }

    public virtual void OnClick()
    { }

    public void OnPointerEnter(PointerEventData data)
    {
        Debug.Log("Gaze Start Focus");
        StartFocus();
    }

    public void OnPointerExit(PointerEventData data)
    {
        Debug.Log("Gaze End Focus");
        EndFocus();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Gaze Click ");
        OnClick();

    }

    public void OnPointerStay(PointerEventData eventData)
    {
        Debug.Log("Gaze Stay Focus");
    }

}
