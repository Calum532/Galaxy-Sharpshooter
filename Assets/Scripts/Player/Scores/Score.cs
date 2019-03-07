﻿using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public TextMeshProUGUI targets;
    public TextMeshProUGUI targetsUI;
    public float targetsDestroyed = 0;
    public float targetsPoints;

    public TextMeshProUGUI clicksTotal;
    public float clicksMissed = 0;
    public float clickCount = 0;

    public TextMeshProUGUI accuracy;
    public TextMeshProUGUI accuracyBonusText;
    public float calcAccuracy = 100f;
    public float accuracyBonus;

    public TextMeshProUGUI score;
    public TextMeshProUGUI highScore;
    public float totalScore = 0;

    private string Name = "Player";

    public GameObject newHighscoreUI;

    void Start()
    {
        highScore.text = PlayerPrefs.GetString("Name", Name) +": "+PlayerPrefs.GetFloat("HighScore", 0).ToString();
    }

    void Update()
    {
        calculateScore();
        clickCount = targetsDestroyed + clicksMissed;
        targets.text = targetsDestroyed.ToString();
        targetsUI.text = targetsDestroyed.ToString();
        clicksTotal.text = clickCount.ToString();
        accuracy.text = calcAccuracy.ToString("00.0") + "%";
        score.text = totalScore.ToString();
    }

    public void setHighScoreHoldersName(string Name)
    {
        PlayerPrefs.SetString("Name", Name);

    }

    public void calculateScore()
    {
        totalScore = targetsPoints * accuracyBonus;

        if(totalScore > PlayerPrefs.GetFloat("HighScore", 0))
        {
            PlayerPrefs.SetFloat("HighScore", totalScore);
            highScore.text = Name+": "+totalScore.ToString();
            newHighscoreUI.SetActive(true);
        }
    }

    public void targetsDestroyedCount()
    {
        //+1 to targets destroyed
        targetsDestroyed = targetsDestroyed + 1;
        targetsPoints = targetsDestroyed * 100;
    }

    public void clicksMissedCount()
    {
        //+1 to clicks missed
        clicksMissed = clicksMissed + 1;
    }

    public void calculateAccuracy()
    {
        calcAccuracy = (targetsDestroyed/clickCount) * 100;

        if (calcAccuracy == 100)
        {
            accuracyBonus = 3f;
            accuracyBonusText.text = "x3";
        }
        else if (calcAccuracy >= 90)
        {
            accuracyBonus = 2f;
            accuracyBonusText.text = "x2";
        }
        else if (calcAccuracy >= 75)
        {
            accuracyBonus = 1.5f;
            accuracyBonusText.text = "x1.5";
        }
        else if (calcAccuracy >= 50)
        {
            accuracyBonus = 1f;
            accuracyBonusText.text = "x1";
        }
        else
        {
            accuracyBonus = 0.5f;
            accuracyBonusText.text = "x0.5";
        }
    }
}
