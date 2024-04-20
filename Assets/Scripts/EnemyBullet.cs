using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    int health;
    public float speed;
    Vector2 _direction;
    bool isReady;
    public int damage = 1;
    private PauseMenu pauseMenu;

    void Awake()
    {
        health = 2;
        isReady = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        SetDireciton(Vector2.left);
        pauseMenu = FindObjectOfType<PauseMenu>();
    }
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

        {
            Vector2 position = transform.position;

            position += _direction * speed * Time.deltaTime;

            transform.position = position;

            Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

            Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

            if ((transform.position.x < min.x-6) || (transform.position.x > max.x) ||
                (transform.position.y < min.y) || (transform.position.y > max.y))

            {
                Destroy(gameObject);
            }
        }
    }

    public void TakeDamage ()
    {
        health--;
        if (health == 0) { Destroy(gameObject); }
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController playerController;
        if (collision.TryGetComponent<PlayerController>(out playerController)) 
        {
            playerController.TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
