using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public enum MoveDirection { LEFT, RIGHT, UP, DOWN }

    public bool isAlive = true;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerMoveAnim(MoveDirection direction)
    {
        switch (direction)
        {
            case MoveDirection.LEFT:
                animator.SetTrigger("Roll_left");
                break;
            case MoveDirection.RIGHT:
                animator.SetTrigger("Roll_right");
                break;
            case MoveDirection.UP:
                animator.SetTrigger("Roll_back");
                break;
            case MoveDirection.DOWN:
                animator.SetTrigger("Roll_forward");
                break;
        }
    }

    public void TrigerDieAnim()
    {
        if (isAlive)
        {
            animator.SetTrigger("Die");
            isAlive = false;
        }
    }

    public void Die()
    {
        UIManager.Instance.ShowEndGameMenu();
    }
}
