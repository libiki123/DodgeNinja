using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerAnimation : MonoBehaviour
{
    public enum MoveDirection { LEFT, RIGHT, UP, DOWN }

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerAnimation(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.LEFT:

                break;
            case MoveDirection.RIGHT:

                break;
            case MoveDirection.UP:
                animator.SetTrigger("Roll_back");
                break;
            case MoveDirection.DOWN:
                 
                break;
        }
    }
}
