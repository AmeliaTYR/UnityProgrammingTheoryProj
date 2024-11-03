using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private bool isPaused = false;
    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0; // Pause the game
        isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1; // Resume the game
        isPaused = false;
    }
}
