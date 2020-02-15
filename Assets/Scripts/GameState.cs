using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        ui = GameObject.Find("GameStatsUI").GetComponent<UIManager>();
    }

    public void UpdatePlayerHealth(int health)
    {
        playerHealth = health;
        ui.UpdateHealthUI(playerHealth);
    }

    public void UpdatePlayerScore(int score)
    {

        playerScore = score;
        ui.UpdateScoreUI(playerScore);
    }

    public void ResetGameState()
    {
        Destroy(this.gameObject);
        Destroy(GameObject.Find("GameStatsUI"));
    }
}
