using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    private TextMeshProUGUI scoreText, healthText;

    private void Start()
    {
        scoreText = GameObject.Find("Score Text").GetComponent<TextMeshProUGUI>();
        if (GameObject.Find("Player Health txt"))
        {
            healthText = GameObject.Find("Player Health txt").GetComponent<TextMeshProUGUI>();
        }
    }

    public void UpdateHealthUI(int health)
    {
        if (!healthText)
        {
            return;
        }
        healthText.text = health.ToString();
    }

    public void UpdateScoreUI(int score)
    {
        if (!scoreText)
        {
            return;
        }
        scoreText.text = score.ToString();
    }
}
