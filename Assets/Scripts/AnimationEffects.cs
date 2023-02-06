using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEffects : MonoBehaviour
{
    [SerializeField] private ParticleSystem landingEffect;

    public void PlayLandingEffect()
    {
        landingEffect.Play();
        AudioManager.instance.PlayOneShot(FMODEvents.instance.spawnDrop, Vector3.zero);
    }
}
