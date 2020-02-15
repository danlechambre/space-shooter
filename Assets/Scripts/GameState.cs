using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private int playerScore = 0;
    private int playerHealth = 0;
    private UIManager ui;

    private void Awake()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    
    private void Start()
    {
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
    }

    private void LocateAndUpdateCurrentUI()
    {
        ui = GameObject.Find("Canvas").GetComponent<UIManager>();
        UpdatePlayerHealth(playerHealth);
        UpdatePlayerScore(playerScore);
    }

    public int GetScore()
    {
        return playerScore;
    }

    public int GetPlayerHealth()
    {
        return playerHealth;
    }

    public void UpdatePlayerHealth(int health)
    {
        if (!ui)
        {
            LocateAndUpdateCurrentUI();
        }
        else
        {
            playerHealth = health;
            ui.UpdateHealthUI(playerHealth);
        }
    }

    public void UpdatePlayerScore(int score)
    {
        if (!ui)
        {
            LocateAndUpdateCurrentUI();
        }
        else
        {
            playerScore = score;
            ui.UpdateScoreUI(playerScore);
        }
    }

    public void ResetGameState()
    {
        Destroy(this.gameObject);
    }
}
