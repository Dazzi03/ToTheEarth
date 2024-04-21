using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;
    public bool IsPaused;
    public GameOverScreen gameOverScreen;

    // Start is called before the first frame update
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOverScreen.gameoverCondition) return;

        if (Input.GetKeyDown(KeyCode.Escape) && VictoryScreen.victoryCondition == false)
        {
            if (IsPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }   
        }
    }

    public void PauseGame()

    {
        if (IsPaused == false)
        { 
            pauseMenu.SetActive(true);
            IsPaused = true;
        }
    }

    public void ResumeGame()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        IsPaused = false;
    }
}
