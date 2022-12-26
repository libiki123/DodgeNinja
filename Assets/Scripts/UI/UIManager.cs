using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using System;
using UnityEngine.UI;
using Unity.Mathematics;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Texts")]
    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private TMP_Text highscoreText;
    [SerializeField] private TMP_Text batteryProgressText;
    [SerializeField] private TMP_Text finalScoreText;

    [Header("Refs")]
    [SerializeField] private GameObject bttnControl;
    [SerializeField] private GameObject swipeControl;
    [SerializeField] private GameObject endGameMenu;
    [SerializeField] private GameObject controlPicker;
    [SerializeField] private RectMask2D batteryProgressBar;

    public event Action PointAdded;
    public int score { get; private set; }
    public int battery { get; private set; }
    public int highScore { get; private set; }


    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        score = 0;
        highScore = 0;
        battery = 0;

        Init();

        GameManager.Instance.DoneLoadScene += Init;
    }

    public void Init()
    {
        scoreText.text = score.ToString();
        battery = GameManager.Instance.saveData.batteryProgress;
        float percentage = math.remap(0, 50, 210, 5, battery);
        batteryProgressBar.padding = new Vector4(percentage, 0, 0, 0);
        batteryProgressText.text = battery.ToString() + " / 50";
        this.highScore = GameManager.Instance.saveData.highScore;
        highscoreText.text = highScore.ToString();

        GameManager.Instance.PauseGame();
        controlPicker.SetActive(true);
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
        batteryProgressText.text = battery.ToString() + " / 50";
        if (battery == 50)
        {
            battery = 0;
        }

    }

    public void ShowEndGameMenu()
    {
        GameManager.Instance.PauseGame();
        endGameMenu.SetActive(true);
        finalScoreText.text = score.ToString();
    }

    public void Replay()
    {
        GameManager.Instance.Restart();
        GameManager.Instance.ResumeGame();
    }

    public void Home()
    {
        GameManager.Instance.ResumeGame();
        GameManager.Instance.LoadMainMenu();
    }

    public void UseButton()
    {
        controlPicker.SetActive(false);
        bttnControl.SetActive(true);
        swipeControl.SetActive(false);
        GameManager.Instance.ResumeGame();
    }

    public void UseSwipe()
    {
        controlPicker.SetActive(false);
        bttnControl.SetActive(false);
        swipeControl.SetActive(true);
        GameManager.Instance.ResumeGame();
    }

    private void OnDestroy()
    {
        GameManager.Instance.DoneLoadScene -= Init;
    }
}
