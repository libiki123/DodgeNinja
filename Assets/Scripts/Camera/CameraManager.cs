using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    private Camera mainCam;
    private bool zoomIn = false;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    public void Start()
    {
        mainCam = Camera.main;
    }

    private void Update()
    {
        if (zoomIn)
        {
            mainCam.fieldOfView -= 20 * Time.deltaTime;

            if (mainCam.fieldOfView <= 56)
            {
                zoomIn = false;
            }
        }
    }

    public void StartZoomIn()
    {
        zoomIn = true;
    }
}
