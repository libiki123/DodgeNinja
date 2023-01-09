using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

[RequireComponent(typeof(Animator))]
public class Player : MonoBehaviour
{
    public enum MoveDirection { LEFT, RIGHT, UP, DOWN }

    public bool isAlive = true;
    [SerializeField] private GameObject impactVFX;

    private Animator animator;

    // Start is called before the first frame update
    void Awake()
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

    public void KillPLayer(Vector3 contactPoint)
    {
        if (isAlive)
        {
            GameObject tmpImpact = Instantiate(impactVFX, contactPoint, Quaternion.identity);
            Destroy(tmpImpact, 1f);
            CameraShaker.Instance.ShakeOnce(4f, 2f, .1f, 1f);
            Handheld.Vibrate();
            animator.SetTrigger("Die");
            isAlive = false;
            DataPersistenceManager.Instance.SaveGame();
        }
    }

    public void Die()
    {
        UIManager.Instance.ShowEndGameMenu();
    }

}
