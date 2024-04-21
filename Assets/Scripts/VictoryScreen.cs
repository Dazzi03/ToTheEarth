using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class VictoryScreen : MonoBehaviour
{
    public GameObject victoryScreen;
    public static bool victoryCondition;
    private PauseMenu pauseMenu;

    //Earth
    public GameObject Earth;
    private GameObject SpawnedEarth;
    public Vector3 earthPositionStart;
    public Vector3 earthPositionEnd;
    public float speed;



    // Start is called before the first frame update
    void Start()
    {
        victoryScreen.SetActive(false);
        victoryCondition = false;
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //VICTORY SCREEN
    public void activateVictoryScreen() 
    {
        victoryScreen.SetActive(true);
        pauseMenu.IsPaused = true;
        victoryCondition = true;
        
        //Destroy all EnemyBullets in Scene
        EnemyBullet[] enemybullets = FindObjectsOfType<EnemyBullet>();
        for (int i = 0; i < enemybullets.Length; i++)
        {
            Destroy(enemybullets[i].gameObject);
        }
        
        //EARTH MOVEMENT
        SpawnedEarth = Instantiate(Earth, earthPositionStart, Quaternion.identity);
        StartCoroutine(MoveTheThing());
    }
    IEnumerator MoveTheThing()
    {
        float t = 0;
        Vector3 moveVector;

        while (t < 1)
        {
            t += Time.deltaTime * speed;
            moveVector = Vector3.Lerp(earthPositionStart, earthPositionEnd, t);
            SpawnedEarth.transform.position = moveVector;
            yield return null;
        }
    }
}
