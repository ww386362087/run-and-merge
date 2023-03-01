using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class ButtonDrag : MonoBehaviour, IPointerDownHandler
{
    public UnityEvent touchEvent = new UnityEvent();


    public void OnPointerDown(PointerEventData eventData)
    {
        touchEvent.Invoke();
    }

    public void SetUpOneEvent(UnityAction action)
    {
        touchEvent.RemoveAllListeners();
        touchEvent.AddListener(action);
    }
}
