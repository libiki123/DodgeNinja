using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapWarning : Trap
{
    public GameObject warningMark;
    public float warningTime = 0.5f;
    public Animator anim;

    public override void SpawnTrap()
    {
        base.SpawnTrap();

        StartCoroutine(StartWarning());
    }

    IEnumerator StartWarning()
    {
        warningMark.SetActive(true);
        yield return new WaitForSecondsRealtime(warningTime);
        warningMark.SetActive(false);
        anim.SetTrigger("Trigger");
    }


}
