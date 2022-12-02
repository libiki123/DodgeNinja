using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool gridMovement = true;

    [Header("Grid Movement Params")]
    [SerializeField] private SwipeControl swipeControl;
    [SerializeField] private float moveSpeed = 0.25f;
    [SerializeField] private float rayLength = 1.4f;    // should be 0.9unit from the player in case it stay neer walls
    [SerializeField] private float rayOffsetX = 0.5f;
    [SerializeField] private float rayOffsetY = 0.5f;
    [SerializeField] private float rayOffsetZ = 0.5f;

    [Header("3D Movement Params")]
    [SerializeField] private FloatingJoystick joystick;
    [SerializeField] private float moveSpeed2 = 2f;
    [SerializeField] private float rotateSpeed = 2f;

    private Rigidbody rb;
    private Vector3 startPos;
    private Vector3 targetPos;
    private Vector3 moveVector;
    private bool moving;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {


        if (gridMovement) GridMove();
        else JoystickMove();
    }

    private void GridMove()
    {
        rb.useGravity = false;
        joystick.gameObject.SetActive(false);
        swipeControl.gameObject.SetActive(true);

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
            }
        }
        else if (swipeControl.swipeDown)
        {
            if (CanMove(Vector3.back))
            {
                targetPos = transform.position + Vector3.back;
                startPos = transform.position;
                moving = true;
            }
        }
        else if (swipeControl.swipeLeft)
        {
            if (CanMove(Vector3.left))
            {
                targetPos = transform.position + Vector3.left;
                startPos = transform.position;
                moving = true;
            }
        }
        else if (swipeControl.swipeRight)
        {
            if (CanMove(Vector3.right))
            {
                targetPos = transform.position + Vector3.right;
                startPos = transform.position;
                moving = true;
            }
        }
    }

    private bool CanMove(Vector3 direction)
    {
        if(Vector3.Equals(Vector3.forward, direction) || Vector3.Equals(Vector3.back, direction))
        {
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.right * rayOffsetX, direction, rayLength)) return false;
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY - Vector3.right * rayOffsetX, direction, rayLength)) return false;
        }
        else if (Vector3.Equals(Vector3.left, direction) || Vector3.Equals(Vector3.right, direction))
        {
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.forward * rayOffsetZ, direction, rayLength)) return false;
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY - Vector3.forward * rayOffsetZ, direction, rayLength)) return false;
        }

        
        return true;
    }

    private void JoystickMove()
    {
        rb.useGravity = false;

        moveVector = Vector3.zero; 
        moveVector.x = joystick.Horizontal * moveSpeed2 * Time.deltaTime;
        moveVector.z = joystick.Vertical * moveSpeed2 * Time.deltaTime;

        if(joystick.Horizontal !=0 || joystick.Vertical != 0)
        {
            Vector3 direction = Vector3.RotateTowards(transform.forward, moveVector, rotateSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(direction);
        }

        rb.MovePosition(rb.position + moveVector);
    }


}
