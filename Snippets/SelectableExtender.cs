using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using System;

public class SelectableExtender : MonoBehaviour, ISelectHandler, IDeselectHandler
{

    public UnityEvent onSelect = new UnityEvent();
    public UnityEvent onDeselect = new UnityEvent();

    public void OnDeselect(BaseEventData eventData)
    {
        onDeselect.Invoke();
    }

    public void OnSelect(BaseEventData eventData)
    {
        onSelect.Invoke();
    }

}
