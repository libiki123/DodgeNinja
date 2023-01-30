using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.Mathematics;
using DG.Tweening;
using Spine.Unity;

public class UIManager : MonoBehaviour, IDataPersistence
{
    public static UIManager instance { get; private set; }

    [Header("Texts")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private TMP_Text batteryProgressText;
    [SerializeField] private TMP_Text finalScoreText;

    [Header("Refs")]
    [SerializeField] private GameObject bttnControl;
    [SerializeField] private GameObject swipeControl;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject endGameMenu;
    [SerializeField] private GameObject controlPicker;

    public event Action PointAdded;
    public int score { get; private set; }
    public int battery { get; private set; }
    public int highScore { get; private set; }

    private int controlType;
    private float fadeTime = 0.6f;


    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        
    }

    public void InitControl()
    {
        if (controlType == 0)
        {
            controlPicker.SetActive(true);
            GameManager.instance.PauseGame();
        }
        else
            StartWihoutPickingControl();
    }

    public void LoadData(GameData data)
    {
        scoreText.text = score.ToString();
        //battery = data.batteryProgress;
        //float percentage = math.remap(0, 50, 285, 5, battery);
        //batteryProgressBar.padding = new Vector4(percentage, 0, 0, 0);
        batteryProgressText.text = battery.ToString();
        this.highScore = data.highScore;
        highscoreText.text = highScore.ToString();
        controlType = data.gameSetting.contronlType;
    }

    public void SaveData(ref GameData data)
    {
        data.totalCoin += score;
        data.batteryProgress += battery;
        data.highScore = highScore;
        data.gameSetting.contronlType = controlType;
    }



    // Update is called once per frame
    public void AddPoint()
    {
        score++;
        scoreText.text = score.ToString();
        if (highScore < score)
        {
            highScore = score;
            highscoreText.text = highScore.ToString();
        }

        PointAdded?.Invoke();
    }

    public void AddBattery()
    {
        battery++;
        batteryProgressText.text = battery.ToString();
        //if (battery == 50)
        //{
        //    battery = 0;
        //}

    }

    public IEnumerator ShowEndGameMenu()
    {
        AudioManager.instance.musicEventInstance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT);
        endGameMenu.transform.GetChild(0).localScale = Vector3.zero;
        endGameMenu.SetActive(true);
        endGameMenu.transform.GetChild(0).DOScale(1.3f, fadeTime).SetEase(Ease.OutElastic);
        finalScoreText.text = score.ToString();
        yield return new WaitForSeconds(fadeTime);
        GameManager.instance.PauseGame();
    }

    public void StartWihoutPickingControl()
    {
        if(controlType == 1)
            bttnControl.SetActive(true);
        else
            swipeControl.SetActive(true);

        GameManager.instance.ResumeGame();
        CameraManager.instance.StartZoomIn();
        AudioManager.instance.InitializeMusic(FMODEvents.instance.gameplayBMG);
    }

    //============================================ Button Event ============================================//
    public void Replay()
    {
        GameManager.instance.ResumeGame();
        GameManager.isRestart = true;
        GameManager.instance.StartGame();
    }

    public void Home()
    {
        GameManager.instance.ResumeGame();
        GameManager.isRestart = false;
        GameManager.instance.LoadMainMenu();
    }

    public void UseButton()
    {
        controlType = 1;
        controlPicker.SetActive(false);
        bttnControl.SetActive(true);
        swipeControl.SetActive(false);
        GameManager.instance.ResumeGame();
        CameraManager.instance.StartZoomIn();
        AudioManager.instance.InitializeMusic(FMODEvents.instance.gameplayBMG);
        DataPersistenceManager.instance.SaveGame();
    }

    public void UseSwipe()
    {
        controlType = 2;
        controlPicker.SetActive(false);
        bttnControl.SetActive(false);
        swipeControl.SetActive(true);
        GameManager.instance.ResumeGame();
        CameraManager.instance.StartZoomIn();
        AudioManager.instance.InitializeMusic(FMODEvents.instance.gameplayBMG);
        DataPersistenceManager.instance.SaveGame();
    }

    public void PauseGame()
    {
        GameManager.instance.PauseGame();
        pauseMenu.SetActive(true);
        AudioManager.instance.musicEventInstance.setPaused(true);
    }

    public void ResumeGame()
    {
        GameManager.instance.ResumeGame();
        pauseMenu.SetActive(false);
        AudioManager.instance.musicEventInstance.setPaused(false);
    }

    public void PlayButtonClickSound()
    {
        AudioManager.instance.PlayOneShot(FMODEvents.instance.buttonClick, Vector3.zero);
    }



}
