using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public SaveSystem SaveSystem;

    bool hasPlayed;
    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        Application.targetFrameRate = 60;
        if (PlayerPrefs.GetInt("FIRSTTIMEOPENING", 1) == 1)
        {
            PlayerPrefs.SetInt("FIRSTTIMEOPENING", 0);
            SaveGame();
        }
        LoadGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Restart()
    {
        SaveGame();
        SceneManager.LoadScene("SampleScene");
    }

    private void SaveGame()
    {
        SaveDataSerialization saveData = new SaveDataSerialization();
        saveData.highScore = ScoreManager.Instance.highScore;
        var jsonFormat = JsonUtility.ToJson(saveData);
        SaveSystem.SaveData(jsonFormat);
    }

    public void LoadGame()
    {
        var jsonFormatData = SaveSystem.LoadData();
        if(String.IsNullOrEmpty(jsonFormatData))
            return;
        SaveDataSerialization saveData = JsonUtility.FromJson<SaveDataSerialization>(jsonFormatData);

        ScoreManager.Instance.Init(saveData.highScore);
    }


}
