using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleBehaviour : StateMachineBehaviour
{
    [SerializeField] private float timeUntilSwitch;
    [SerializeField] private int numberOfIdleAnim;

    private bool isIdle;
    private float idleTime;  // idle for how long
    private int idleAnimIndex = 0;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        ResetIdle();
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       if(isIdle == false)
       {
            idleTime += Time.deltaTime;

            if(idleTime > timeUntilSwitch && stateInfo.normalizedTime % 1 < 0.02f)
            {
                isIdle = true;
                idleAnimIndex = Random.Range(1, numberOfIdleAnim + 1);
                idleAnimIndex = idleAnimIndex * 2 - 1;

                animator.SetFloat("IdleBlend", idleAnimIndex - 1);
            }
       }
       else if(stateInfo.normalizedTime % 1 > 0.98f)
       {
            ResetIdle();
       }

        animator.SetFloat("IdleBlend", idleAnimIndex, 0.05f, Time.deltaTime);
    }

    private void ResetIdle()
    {
        if (isIdle)
        {
            idleAnimIndex--;
        }

        isIdle = false;
        idleTime = 0;
    }
}
