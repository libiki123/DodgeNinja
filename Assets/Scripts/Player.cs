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
    [SerializeField] private Shop_SO skinData;

    private Animator animator;
    private SkinnedMeshRenderer SMR;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        SMR = GetComponentInChildren<SkinnedMeshRenderer>();

        GameManager.Instance.DoneLoadScene += UpdateSkin;
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
        }
    }

    public void Die()
    {
        UIManager.Instance.ShowEndGameMenu();
    }

    public void UpdateSkin()
    {
        Debug.Log(GameManager.Instance.saveData.currentSkinId);
        if(GameManager.Instance.saveData.currentSkinId == "")
        {
            SMR.sharedMesh = skinData.skins[0].mesh;
            SMR.material = skinData.skins[0].material;
        }

        foreach(var skin in skinData.skins)
        {
            if(skin.id == GameManager.Instance.saveData.currentSkinId)
            {
                SMR.sharedMesh = skin.mesh;
                SMR.material = skin.material;
            }
        }
    }

    public void OnDestroy()
    {
        GameManager.Instance.DoneLoadScene -= UpdateSkin;
    }
}
