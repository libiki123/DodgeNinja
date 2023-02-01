using EZCameraShake;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWarning : Trap
{
    public GameObject rootParent;
    [SerializeField] private GameObject warningMark;
    [SerializeField] private float warningTime = 0.5f;
    [SerializeField] private float dropStartTime = 2f;
    [SerializeField] private float explodeSoundDelay = 0.25f;
    [SerializeField] private Animator anim;

    public override void SpawnTrap()
    {
        base.SpawnTrap();

        StartCoroutine(StartWarning());
        Invoke("StartAnim", dropStartTime);
    }

    IEnumerator StartWarning()
    {
        warningMark.SetActive(true);
        yield return new WaitForSecondsRealtime(warningTime);
        warningMark.SetActive(false);
    }

    void StartAnim()
    {
        anim.SetTrigger("Trigger");
        Invoke("PlayExplodeSound", explodeSoundDelay);
        StartCoroutine(Utils.CheckAnimationCompleted(anim, "Spikeball_Drop", () =>
        {
            rootParent.SetActive(false);
        }
        ));
    }

    void PlayExplodeSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.trapDroped, transform.position);
        CameraShaker.Instance.ShakeOnce(2f, 1f, .1f, .3f);
    }
}
