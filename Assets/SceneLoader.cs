using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public bool isMainMenu = false;
    private SkeletonGraphic skgp;

    private void Awake()
    {
        skgp = gameObject.GetComponent<SkeletonGraphic>();
    }

    private void Start()
    {
        if (!isMainMenu)
        {
            StartCoroutine(StartTransition());
        }
    }

    public IEnumerator StartTransition()
    {
        skgp.enabled = true;
        skgp.AnimationState.SetAnimation(0, "animation2", false);
        yield return new WaitForSeconds(skgp.Skeleton.Data.FindAnimation("animation2").Duration);
        UIManager.instance.InitControl();
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<Player>().SpawnPlayer();
        
    }

    public IEnumerator EndTransition()
    {
        skgp.enabled = true;
        skgp.AnimationState.SetAnimation(0, "animation1", false);
        yield return new WaitForSeconds(skgp.Skeleton.Data.FindAnimation("animation1").Duration);
        GameManager.instance.StartGame();
    }

}
