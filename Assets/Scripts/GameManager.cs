using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //public event Action RestartGame;
    public static GameManager Instance { get; private set; }
    public SaveSystem SaveSystem;

    public string gameSceneName;
    public SaveDataSerialization saveData { get; private set; }
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
            
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;

        var jsonFormatData = SaveSystem.LoadData();
        if (String.IsNullOrEmpty(jsonFormatData)) SaveGame();

        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        GetScore();
        SaveGame();
        SceneManager.LoadScene(gameSceneName);
    }

    private void UpdateSaveData(int coin)
    {
        //saveData
    }

    private void GetScore()
    {
        saveData.totalCoin += ScoreManager.Instance.score;
        saveData.highScore = ScoreManager.Instance.highScore;
        saveData.batteryProgress = ScoreManager.Instance.battery;
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

        if(ScoreManager.Instance) ScoreManager.Instance.Init();
        if(MainMenu.Instance) MainMenu.Instance.Init();
    }


}
