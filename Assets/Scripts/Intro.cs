using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    public float waitTime = 3;

    void Start()
    {
        StartCoroutine(WaitTillIntroFinish());
    }

    private IEnumerator WaitTillIntroFinish()
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadSceneAsync("MainMenu");
    }
}
