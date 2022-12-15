using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public TMP_Text scoreText;
    public TMP_Text highscoreText;
    public TMP_Text batteryProgressText;

    public int score { get; private set; }
    public int battery { get; private set; }
    public int highScore { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        score = 0;
        highScore = 0;
        battery = 0;
    }

    public void Init()
    {
        scoreText.text = score.ToString();
        battery = GameManager.Instance.saveData.batteryProgress;
        batteryProgressText.text = battery.ToString() + " / 50";
        this.highScore = GameManager.Instance.saveData.highScore;
        highscoreText.text = "HIGHSCORE " + highScore.ToString();

    }

    // Update is called once per frame
    public void AddPoint(bool isBattery)
    {
        if (isBattery)
        {
            battery++;
            batteryProgressText.text = battery.ToString() + " / 50";
            if (battery == 50)
            {
                battery = 0;
            }   
        }
        else
        {
            score++;
            scoreText.text = score.ToString();
            if (highScore < score)
            {
                highScore = score;
                highscoreText.text = "HIGHSCORE " + highScore.ToString();
            }
        }
    }


}
