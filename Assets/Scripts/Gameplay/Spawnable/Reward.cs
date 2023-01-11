using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Reward : MonoBehaviour
{
    [SerializeField] private bool isCoin;
    [SerializeField] private GameObject collectedVFX;
    public event Action OnRewardCollected;

    private EventReference rewardCollectedSound;

    private void Start()
    {
        rewardCollectedSound = isCoin ? FMODEvents.instance.coinCollected : FMODEvents.instance.scrollCollected;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            GameObject vfx = Instantiate(collectedVFX, transform.position, Quaternion.identity);
            Destroy(vfx, 1f);
            AudioManager.instance.PlayOneShot(rewardCollectedSound, transform.position);
            gameObject.SetActive(false);
            OnRewardCollected?.Invoke();
        }
    }

}
