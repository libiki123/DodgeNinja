using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event Action DoneLoadScene;
    public static GameManager instance { get; private set; }

    public string gameSceneName;
    public string menuSceneName;
    public static bool isRestart = false;


    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
            
    }

    void Start()
    {
        Application.targetFrameRate = 30;
    }

    public void StartGame()
    {
        loadNewScene(gameSceneName);
    }

    public void LoadMainMenu()
    {
        loadNewScene(menuSceneName);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    public void loadNewScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);

        if (SceneManager.GetActiveScene().name != sceneName)
        {
            StartCoroutine("waitForSceneLoad", sceneName);
        }
    }

    IEnumerator waitForSceneLoad(string sceneName)
    {
        while (SceneManager.GetActiveScene().name != sceneName)
        {
            yield return null;
        }

        // Do anything after proper scene has been loaded
        if (SceneManager.GetActiveScene().name == sceneName)
        {
            DoneLoadScene?.Invoke();
        }
    }
}
