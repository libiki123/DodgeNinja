using System.Collections;
using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ButtonEvent : Button
{
    public event Action buttonPressed;
    public event Action buttonReleased;

    // Button is Pressed
    public override void OnPointerDown(PointerEventData eventData)
    {
        base.OnPointerDown(eventData);
        buttonPressed?.Invoke();
    }

    // Button is released
    public override void OnPointerUp(PointerEventData eventData)
    {
        base.OnPointerUp(eventData);
        buttonReleased?.Invoke();
    }

}
