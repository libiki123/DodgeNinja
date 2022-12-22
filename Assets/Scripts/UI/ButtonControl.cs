using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonControl : MonoBehaviour
{
    public Button Up;
    public Button Down;
    public Button Left;
    public Button Right;

    [HideInInspector] public bool tapUp, tapRight, tapLeft, tapDown;

    public void TapUp()
    {
        tapUp = true;
    }

    public void TapDown()
    {
        tapDown = true;
    }
    public void TapLeft()
    {
        tapLeft = true;
    }
    public void TapRight()
    {
        tapRight = true;
    }

    public void Reset()
    {
        tapUp = false;
        tapDown = false;
        tapLeft = false;
        tapRight = false;
    }
}
