using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Grid Movement Params")]
    [SerializeField] private SwipeControl swipeControl;
    [SerializeField] private ButtonControl buttonControl;
    [SerializeField] private float moveSpeed = 0.25f;
    [SerializeField] private float rayLength = 1.4f;    // should be 0.9unit from the player in case it stay neer walls
    [SerializeField] private float rayOffsetX = 0.5f;
    [SerializeField] private float rayOffsetY = 0.5f;
    [SerializeField] private float rayOffsetZ = 0.5f;

    private Vector3 minBound = new Vector3(-2.5f, 0, -2.5f);
    private Vector3 maxBound = new Vector3(2.5f, 0, 2.5f);
    private Vector3 startPos;
    private Vector3 targetPos;
    private bool moving = false;

    private Player player;

    private void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
       GridMove();
    }

    private void GridMove()
    {
        if (!player.isAlive) return;

        if (moving)
        {
            if (Vector3.Distance(startPos, transform.position) > 0.7f)
            {
                transform.position = targetPos;
                
                moving = false;
                if (buttonControl.gameObject.activeSelf) buttonControl.Reset();
                return;
            }

            transform.position += (targetPos - startPos) * moveSpeed * Time.fixedDeltaTime;
            return;
        }
        

        if (swipeControl.swipeUp || buttonControl.tapUp || Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (CanMove(Vector3.forward))
            {
                targetPos = transform.position + Vector3.forward;
                startPos = transform.position;
                moving = true;
                player.TriggerMoveAnim(Player.MoveDirection.UP);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.dash, startPos);
            }
            else
            {
                player.TriggerMoveAnim(Player.MoveDirection.UP);
                if (buttonControl.gameObject.activeSelf) buttonControl.Reset();
            }
        }
        else if (swipeControl.swipeDown || buttonControl.tapDown || Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (CanMove(Vector3.back))
            {
                targetPos = transform.position + Vector3.back;
                startPos = transform.position;
                moving = true;
                player.TriggerMoveAnim(Player.MoveDirection.DOWN);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.dash, startPos);
            }
            else
            {
                player.TriggerMoveAnim(Player.MoveDirection.DOWN);
                if (buttonControl.gameObject.activeSelf) buttonControl.Reset();
            }
        }
        else if (swipeControl.swipeLeft || buttonControl.tapLeft || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (CanMove(Vector3.left))
            {
                targetPos = transform.position + Vector3.left;
                startPos = transform.position;
                moving = true;
                player.TriggerMoveAnim(Player.MoveDirection.LEFT);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.dash, startPos);
            }
            else
            {
                player.TriggerMoveAnim(Player.MoveDirection.LEFT);
                if (buttonControl.gameObject.activeSelf) buttonControl.Reset();
            }
        }
        else if (swipeControl.swipeRight || buttonControl.tapRight || Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (CanMove(Vector3.right))
            {
                targetPos = transform.position + Vector3.right;
                startPos = transform.position;
                moving = true;
                player.TriggerMoveAnim(Player.MoveDirection.RIGHT);
                AudioManager.instance.PlayOneShot(FMODEvents.instance.dash, startPos);
            }
            else
            {
                player.TriggerMoveAnim(Player.MoveDirection.RIGHT);
                if (buttonControl.gameObject.activeSelf) buttonControl.Reset();
            }
        }

        
    }

    private bool CanMove(Vector3 direction)
    {
        int layermask = 1 << LayerMask.NameToLayer("Obstacle");

        if (Vector3.Equals(Vector3.forward, direction) || Vector3.Equals(Vector3.back, direction))
        {
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.right * rayOffsetX, direction, rayLength, layermask)) return false;
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY - Vector3.right * rayOffsetX, direction, rayLength, layermask)) return false;
        }
        else if (Vector3.Equals(Vector3.left, direction) || Vector3.Equals(Vector3.right, direction))
        {
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY + Vector3.forward * rayOffsetZ, direction, rayLength, layermask)) return false;
            if (Physics.Raycast(transform.position + Vector3.up * rayOffsetY - Vector3.forward * rayOffsetZ, direction, rayLength, layermask)) return false;
        }

        targetPos = transform.position + direction;
        if (targetPos.x > maxBound.x || targetPos.x < minBound.x || targetPos.z > maxBound.z || targetPos.z < minBound.z)
        {
            return false;
        }

        return true;
    }


}
