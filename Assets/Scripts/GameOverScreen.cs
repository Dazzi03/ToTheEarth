using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScreen : MonoBehaviour
{
    public GameObject gameoverScreen;
    private PauseMenu pauseMenu;
    public bool gameoverCondition;

    // Start is called before the first frame update
    void Start()
    {
        gameoverScreen.SetActive(false);
        gameoverCondition = false;

        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //GAME-OVER SCREEN: ON
    public void activateGameOverScreen()
    {
        gameoverScreen.SetActive(true);
        pauseMenu.IsPaused = true;
        gameoverCondition = true;

        //DESTROY ALL METEORS ON SCREEN IF PRESENT
        EnemyBullet[] enemybullets = FindObjectsOfType<EnemyBullet>();
        for (int i = 0; i < enemybullets.Length; i++)
        {
            Destroy(enemybullets[i].gameObject);
        }

    }
}
