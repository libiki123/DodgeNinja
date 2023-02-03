using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(TextMeshProUGUI))]
public class NumberCounter : MonoBehaviour
{
    private TextMeshProUGUI text;
    [SerializeField] private int countFPS = 30;
    [SerializeField] private float duration = 1f;
    [SerializeField] private string numberFormat = "N0";

    private int _value;
    public int value
    {
        get
        {
            return _value;
        }
        set
        {
            UpdateText(value);
            _value = value; 
        }
    }

    private Coroutine countringCoroutine;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void UpdateText(int newValue)
    {
        if(countringCoroutine != null)
        {
            StopAllCoroutines();
        }

        countringCoroutine = StartCoroutine(CountText(newValue));
    }

    private IEnumerator CountText(int newValue)
    {
        WaitForSeconds wait = new WaitForSeconds(1f / countFPS);
        int previousValue = _value;
        int stepAmount;

        if(newValue - previousValue < 0)
        {
            stepAmount = Mathf.FloorToInt((newValue - previousValue) / (countFPS * duration));
        }
        else
        {
            stepAmount = Mathf.CeilToInt((newValue - previousValue) / (countFPS * duration));
        }

        if(previousValue < newValue)
        {
            while(previousValue < newValue)
            {
                previousValue += stepAmount;
                if(previousValue > newValue)
                {
                    previousValue = newValue;
                }

                text.SetText(previousValue.ToString(numberFormat));

                yield return wait;
            }
        }
        else
        {
            while (previousValue > newValue)
            {
                previousValue += stepAmount;
                if (previousValue < newValue)
                {
                    previousValue = newValue;
                }

                text.SetText(previousValue.ToString(numberFormat));

                yield return wait;
            }
        }
    }

    public void SetDirectValue(int newValue)
    {
        _value = newValue;
    }
}
