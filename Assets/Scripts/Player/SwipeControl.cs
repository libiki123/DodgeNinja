using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeControl : MonoBehaviour
{
    public float swipeRange = 50f;
    public float tapRange = 10f;
    [SerializeField] GameObject swipeTrailPrefab;

    [HideInInspector] public bool tap, swipeLeft, swipeRight, swipeUp,swipeDown;

    private Vector2 startTouchPos;
    private Vector2 currentPos;
    private Vector2 endTouchPos;
    private bool stopTouch = false;
    private RectTransform swipableArea;
    private Camera mainCam;
    private GameObject currentTrail;

    private void Start()
    {
        swipableArea = GetComponent<RectTransform>();
        mainCam = Camera.main;
        //swipableArea.sizeDelta = 
    }

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

            Vector3 screenPos = new Vector3(startTouchPos.x, startTouchPos.y, 12f);
            Vector3 touchPos = mainCam.ScreenToWorldPoint(screenPos);
            currentTrail = Instantiate(swipeTrailPrefab, touchPos, Quaternion.identity);
        }

        if (!RectTransformUtility.RectangleContainsScreenPoint(swipableArea, startTouchPos))
            return;

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

            Vector3 screenPos = new Vector3(currentPos.x, currentPos.y, 12f);
            Vector3 touchPos = mainCam.ScreenToWorldPoint(screenPos);
            currentTrail.transform.position = touchPos;

        }

        if(Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
        {
            stopTouch = false;
            endTouchPos = Input.GetTouch(0).position;
            Vector2 distance = endTouchPos - startTouchPos;
            Destroy(currentTrail, 0.5f);

            if(Mathf.Abs(distance.x) < tapRange && Mathf.Abs(distance.y) < tapRange) // Tap
            {
                tap = true;
            }
        }
    }
}
