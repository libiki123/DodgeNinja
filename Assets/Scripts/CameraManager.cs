using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;

    private Camera mainCam;
    private bool zoomIn = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
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
