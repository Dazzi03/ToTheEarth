using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    Vector2 _direction;
    bool isReady;
    public float speed;
    private PauseMenu pauseMenu;
    EnemyBullet enemyBullet;
    BlackHole blackHole;

    void Start()
    {
        enemyBullet = FindObjectOfType<EnemyBullet>();
        blackHole = FindObjectOfType<BlackHole>();
        SetDireciton(Vector2.right);

        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    //PLAYER'S PROJECTILE DIRECTION
    public void SetDireciton(Vector2 direction)
    {
        _direction = direction.normalized;

        isReady = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu.IsPaused == true) return;

        if (isReady)

        //PLAYER'S PROJECTILE MOVEMENT AND DESPAWN
        {
            Vector2 position = transform.position;
            position += _direction * speed * Time.deltaTime;
            transform.position = position;
           
            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            if ((transform.position.x < min.x - 6) || (transform.position.x > max.x) ||
                (transform.position.y < min.y) || (transform.position.y > max.y))

            {
                Destroy(gameObject);
            }
        }
    }

    //PLAYER'S PROECTILE HITTING
    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "EnemyBulletTag"))
        {
            col.GetComponent<EnemyBullet>().TakeDamage();
            Destroy(gameObject);
        }
        if ((col.tag == "BlackHoleTag"))
        {
            blackHole.TakeDamage(1);
            Destroy(gameObject);
        }
    }
}
