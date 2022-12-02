using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    [HideInInspector] public bool tap, swipeLeft, swipeRight, swipeUp,swipeDown;

    private Vector2 startTouchPos;
    private Vector2 currentPos;
    private Vector2 endTouchPos;
    private bool stopTouch = false;

    public float swipeRange = 50f;
    public float tapRange = 10f;

    // Update is called once per frame
    void Update()
    {
        Swipe(); 
    }
    
    public void Swipe()
    {
        tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            startTouchPos = Input.GetTouch(0).position;
        }

        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            currentPos = Input.GetTouch(0).position;
            Vector2 distance = currentPos - startTouchPos;

            if (!stopTouch)
            {
                if (distance.x < -swipeRange)       // Left
                {
                    swipeLeft = true;
                    stopTouch = true;
                }
                else if (distance.x > swipeRange)   //Right
                {
                    swipeRight = true;
                    stopTouch = true;
                }
                else if (distance.y > swipeRange)   // Uo
                {
                    swipeUp = true;
                    stopTouch = true;
                }
                else if (distance.y < -swipeRange)  // Down
                {
                    swipeDown = true;
                    stopTouch = true;
                }
            }
        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
            endTouchPos = Input.GetTouch(0).position;
            Vector2 distance = endTouchPos - startTouchPos;

            if(Mathf.Abs(distance.x) < tapRange && Mathf.Abs(distance.y) < tapRange) // Tap
            {
                tap = true;
            }
        }
    }
}
