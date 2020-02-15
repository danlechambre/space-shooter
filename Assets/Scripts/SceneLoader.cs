using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public void LoadGameScene()
    {
        
        SceneManager.LoadScene(2);
    }

    public void LoadStartMenu()
    {
        SceneManager.LoadScene(0);
        FindObjectOfType<GameState>().ResetGameState();
    }

    public void QuitGame()
    {
       
    }

    public void LoadGameOver(float timeToWait)
    {
        StartCoroutine(GameOverSequence(timeToWait));
    }

    public IEnumerator GameOverSequence(float timeToWait)
    {
        yield return new WaitForSeconds(2.0f);
        SceneManager.LoadScene(1);
    }
}
