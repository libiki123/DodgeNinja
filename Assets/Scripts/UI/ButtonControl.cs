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

    public bool tapUp, tapRight, tapLeft, tapDown;

    //private void Start()
    //{
    //    Up.buttonPressed += PressUp;
    //    Up.buttonReleased += RelasseUp;
    //    Down.buttonPressed += PressDown;
    //    Down.buttonReleased += ReleaseDown;
    //    Left.buttonPressed += PressLeft;
    //    Left.buttonReleased += ReleaseLeft;
    //    Right.buttonPressed += PressRight;
    //    Right.buttonReleased += ReleaseRight;
    //}

    public void PressUp()
    {
        tapUp = true;
    }

    public void RelasseUp()
    {
        tapUp = false;
    }

    public void PressDown()
    {
        tapDown = true;
    }

    public void ReleaseDown()
    {
        tapDown = false;
    }

    public void PressLeft()
    {
        tapLeft = true;
    }

    public void ReleaseLeft()
    {
        tapLeft = false;
    }

    public void PressRight()
    {
        tapRight = true;
    }

    public void ReleaseRight()
    {
        tapRight = false;
    }

    public void Reset()
    {
        tapUp = false;
        tapDown = false;
        tapLeft = false;
        tapRight = false;
    }

    //private void OnDestroy()
    //{
    //    Up.buttonPressed -= PressUp;
    //    Up.buttonReleased -= RelasseUp;
    //    Down.buttonPressed -= PressDown;
    //    Down.buttonReleased -= ReleaseDown;
    //    Left.buttonPressed -= PressLeft;
    //    Left.buttonReleased -= ReleaseLeft;
    //    Right.buttonPressed -= PressRight;
    //    Right.buttonReleased -= ReleaseRight;
    //}
}
