using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public event Action DoneLoadScene;
    public static GameManager Instance { get; private set; }
    public SaveSystem SaveSystem;

    public string gameSceneName;
    public string menuSceneName;
    public SaveDataSerialization saveData { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            gameObject.SetActive(false);
            Destroy(this);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 30;

        var jsonFormatData = SaveSystem.LoadData();
        if (String.IsNullOrEmpty(jsonFormatData)) SaveGame();

        LoadGame();
        //MainMenu.Instance.Init();
    }

    public void Restart()
    {
        SaveGameResult();
        StartGame();
    }

    public void StartGame()
    {
        LoadGame();
        loadNewScene(gameSceneName);
    }

    public void LoadMainMenu()
    {
        SaveGameResult();
        LoadGame();
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

    private void SaveGameResult()
    {
        saveData.totalCoin += UIManager.Instance.score;
        saveData.highScore = UIManager.Instance.highScore;
        saveData.batteryProgress = UIManager.Instance.battery;
        SaveGame();
    }

    private void SaveMoneSpent()
    {
        saveData.totalCoin = MainMenu.Instance.totalCoin;
        SaveGame();
    }

    private void SaveGame()
    {
        var jsonFormat = JsonUtility.ToJson(saveData);
        SaveSystem.SaveData(jsonFormat);
    }

    public void LoadGame()
    {
        var jsonFormatData = SaveSystem.LoadData();
        if(String.IsNullOrEmpty(jsonFormatData))
            return;
        saveData = JsonUtility.FromJson<SaveDataSerialization>(jsonFormatData);
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
