using Spine.Unity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    public bool isMainMenu = false;
    private SkeletonGraphic skgp;
    private GameObject player;

    private void Awake()
    {
        skgp = gameObject.GetComponent<SkeletonGraphic>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void Start()
    {
        DataPersistenceManager.instance.RefreshDataPersistenceObjs();
        DataPersistenceManager.instance.LoadGame();

        if (!isMainMenu && !GameManager.isRestart)
        {
            StartCoroutine(StartTransition());
        }
        else if(!isMainMenu)
        {
            StartCoroutine(StartWithoutTransition());
        }
            
    }

    public IEnumerator StartTransition()
    {
        skgp.enabled = true;
        skgp.AnimationState.SetAnimation(0, "animation2", false);
        AudioManager.instance.PlayOneShot(FMODEvents.instance.doorSlice, transform.position);
        yield return new WaitForSeconds(skgp.Skeleton.Data.FindAnimation("animation2").Duration - 0.08f);
        UIManager.instance.InitControl();
        player.GetComponent<Player>().SpawnPlayer();
    }

    public IEnumerator EndTransition()
    {
        skgp.enabled = true;
        skgp.AnimationState.SetAnimation(0, "animation1", false);
        yield return new WaitForSeconds(skgp.Skeleton.Data.FindAnimation("animation1").Duration);
        GameManager.instance.StartGame();
    }

    public IEnumerator StartWithoutTransition()
    {
        yield return new WaitForSeconds(0.05f);
        UIManager.instance.InitControl();
        player.GetComponent<Player>().SpawnPlayer();
    }

}
