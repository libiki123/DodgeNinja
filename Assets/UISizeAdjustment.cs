using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RectTransform))]
public class UISizeAdjustment : MonoBehaviour
{
    [SerializeField] private Vector2 widthHeight;
    [SerializeField] private Vector2 screenPercentage;

    private RectTransform ET;
    

    private void Awake()
    {
        ET = GetComponent<RectTransform>(); 
    }

    private void Start()
    {
        float x;
        float y;

        if(widthHeight != Vector2.zero)
        {
            x = widthHeight.x == 0? ET.sizeDelta.x : widthHeight.x;
            y = widthHeight.y == 0 ? ET.sizeDelta.y : widthHeight.y;
            ET.sizeDelta = new Vector2 (x, y);
        }
        else if(screenPercentage != Vector2.zero)
        {
            RectTransform canvasRT = transform.root.GetComponent<RectTransform>();
            x = screenPercentage.x == 0 ? ET.sizeDelta.x : canvasRT.rect.width * screenPercentage.x / 100f;
            y = screenPercentage.y == 0 ? ET.sizeDelta.y : canvasRT.rect.height * screenPercentage.y / 100f;
            ET.sizeDelta = new Vector2(x, y);
        }
    }
}
