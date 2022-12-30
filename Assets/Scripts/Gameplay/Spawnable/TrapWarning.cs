using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWarning : Trap
{
    public GameObject rootParent;
    public GameObject warningMark;
    public float warningTime = 0.5f;
    public float dropStartTime = 2f;
    public Animator anim;

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
        StartCoroutine(Utils.CheckAnimationCompleted(anim, "Spikeball_Drop", () =>
        {
            rootParent.SetActive(false);
        }
        ));
    }


}
