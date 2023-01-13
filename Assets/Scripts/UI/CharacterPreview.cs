using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPreview : MonoBehaviour
{
    [SerializeField] private Transform character;
    [SerializeField] private float rotationSpeed = 0.1f;

    private RectTransform rotateRect;
    private Touch touch;
    private Camera cam;

    private void Start()
    {
        rotateRect = GetComponent<RectTransform>();
        cam = Camera.main;
        rotateRect.sizeDelta = new Vector2(rotateRect.sizeDelta.x, Screen.height * 0.4f);
    }

    void Update()
    {
        if(Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            
            if (!RectTransformUtility.RectangleContainsScreenPoint(rotateRect, touch.position, cam))
                return;

            if (touch.phase == TouchPhase.Moved)
            {
                //transform.DORotate(new Vector3(transform.rotation.x, -touch.deltaPosition.x * rotationSpeed, transform.rotation.z), 0.1f, RotateMode.FastBeyond360);

                character.rotation = Quaternion.Euler(0f, -touch.deltaPosition.x * rotationSpeed, 0f) * character.rotation;
            }
        }
    }
}
