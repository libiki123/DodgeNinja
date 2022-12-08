using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance { get; private set; }

    public TMP_Text scoreText;
    public TMP_Text highscoreText;

    public int score { get; private set; }
    public int highScore { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;

        score = 0;
        highScore = 0;
    }

    public void Init(int highScore)
    {
        scoreText.text = score.ToString();
        this.highScore = highScore;
        highscoreText.text = "HIGHSCORE " + highScore.ToString();
    }

    // Update is called once per frame
    public void AddPoint()
    {
        score += 1;
        scoreText.text = score.ToString();
        if(highScore < score)
            highScore = score;
    }


}
