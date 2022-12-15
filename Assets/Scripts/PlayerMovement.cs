using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Grid Movement Params")]
    [SerializeField] private SwipeControl swipeControl;
    [SerializeField] private float moveSpeed = 0.25f;
    //[SerializeField] private float rayLength = 1.4f;    // should be 0.9unit from the player in case it stay neer walls
    //[SerializeField] private float rayOffsetX = 0.5f;
    //[SerializeField] private float rayOffsetY = 0.5f;
    //[SerializeField] private float rayOffsetZ = 0.5f;

    Vector3 minBound = new Vector3(-2.5f, 0, -2.5f);
    Vector3 maxBound = new Vector3(2.5f, 0, 2.5f);
    private Vector3 startPos;
    private Vector3 targetPos;
    private bool moving;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
       GridMove();
    }

    private void GridMove()
    {
        if (moving)
        {
            if (Vector3.Distance(startPos, transform.position) > 1f)
            {
                transform.position = targetPos;
                moving = false;
                return;
            }

            transform.position += (targetPos - startPos) * moveSpeed * Time.deltaTime;
            return;
        }
        
        if (swipeControl.swipeUp) //(Input.GetKeyDown(KeyCode.W))
        {
            if (CanMove(Vector3.forward))
            {
                targetPos = transform.position + Vector3.forward;
                startPos = transform.position;
                moving = true;
                animator.SetTrigger("Roll_back");
            }
        }
        else if (swipeControl.swipeDown)
        {
            if (CanMove(Vector3.back))
            {
                targetPos = transform.position + Vector3.back;
                startPos = transform.position;
                moving = true;
                animator.SetTrigger("Roll_forward");
            }
        }
        else if (swipeControl.swipeLeft)
        {
            if (CanMove(Vector3.left))
            {
                targetPos = transform.position + Vector3.left;
                startPos = transform.position;
                moving = true;
                animator.SetTrigger("Roll_left");
            }
        }
        else if (swipeControl.swipeRight)
        {
            if (CanMove(Vector3.right))
            {
                targetPos = transform.position + Vector3.right;
                startPos = transform.position;
                moving = true;
                animator.SetTrigger("Roll_right");
            }
        }
    }

    private bool CanMove(Vector3 direction)
    {
        //if(Vector3.Equals(Vector3.forward, direction) || Vector3.Equals(Vector3.back, direction))
        //{
        //    if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.right * rayOffsetX, direction, rayLength)) return false;
        //    if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY - Vector3.right * rayOffsetX, direction, rayLength)) return false;
        //}
        //else if (Vector3.Equals(Vector3.left, direction) || Vector3.Equals(Vector3.right, direction))
        //{
        //    if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.forward * rayOffsetZ, direction, rayLength)) return false;
        //    if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY - Vector3.forward * rayOffsetZ, direction, rayLength)) return false;
        //}

        targetPos = transform.position + direction;
        if (targetPos.x > maxBound.x || targetPos.x < minBound.x || targetPos.z > maxBound.z || targetPos.z < minBound.z)
        {
            return false;
        }

        return true;
    }


}
