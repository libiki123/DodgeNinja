using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Reward : MonoBehaviour
{
    [SerializeField] private EventReference rewardCollectedSound;
    [SerializeField] private GameObject collectedVFX;
    public event Action OnRewardCollected;

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
