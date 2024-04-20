using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole : MonoBehaviour
{
    private float meteorTimer = 0;
    public float meteorTime = 1;
    public float minMeteorTime = 0.5f, maxMeteorTime = 2f;
    public EnemyBullet meteorPrefab;
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Transform currentPoint;
    public float speed;
    public int maxHealth = 20;
    public int currentHealth;
    public bool blackHole;
    private PauseMenu pauseMenu;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentPoint = pointB.transform;
        currentHealth = maxHealth;
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            FindObjectOfType<VictoryScreen>().activateVictoryScreen();
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()

    {
        if (pauseMenu.IsPaused == true) 
        {
            rb.velocity = Vector2.zero;
            return; 
        }

        {

            Vector2 point = currentPoint.position - transform.position;
            if (currentPoint == pointB.transform)
            {
                rb.velocity = new Vector2(0, -speed);
            }
            else
            {
                rb.velocity = new Vector2(0, speed);
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointB.transform)
            {
                currentPoint = pointA.transform;
            }

            if (Vector2.Distance(transform.position, currentPoint.position) < 0.5f && currentPoint == pointA.transform)
            {
                currentPoint = pointB.transform;
            }

            if (meteorTimer >= meteorTime)
            {
                meteorTimer = 0;
                meteorTime = Random.Range(minMeteorTime, maxMeteorTime);
                EnemyBullet meteor = GameObject.Instantiate(meteorPrefab);
                meteor.transform.position = transform.position;
            }
            meteorTimer += Time.deltaTime;
        }
    }
}
