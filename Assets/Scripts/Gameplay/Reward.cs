using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Reward : MonoBehaviour
{
    public event Action OnRewardCollected;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("OBTAIN");
            
            // Increase coin

            gameObject.SetActive(false);
            OnRewardCollected?.Invoke();
        }
    }
}
