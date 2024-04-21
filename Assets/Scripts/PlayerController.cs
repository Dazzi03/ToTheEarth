using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Speed
    public float speed;

    //Score
    public int score = 0;

    //Shooting
    public Transform FirePoint;
    public GameObject bullet;
    private float ShootCountdown;
    public float ShootTime;

    //Men�
    private PauseMenu pauseMenu;
    private GameOverScreen gameoverScreen;

    //Health
    public int maxHealth = 3;
    public int currentHealth;
    [SerializeField] private Transform healthContainer;

    //Flickering
    public float invincibilityDuration;
    private Animator animator;
    private Collider2D collider2D;

    //Shield
    public Image ShieldCountdownBar;
    public Transform shieldIcon;
    public float shieldTimer;
    public float shieldTime;
    public float shieldCooldown;
    public bool isShieldActive;
    

    // Start is called before the first frame update
    void Start()
    {
        ShootCountdown = 0;
        currentHealth = maxHealth;

        animator = GetComponent<Animator>();
        collider2D = GetComponent<Collider2D>();

        //Shield
        shieldTimer = -1;
        isShieldActive = false;
        shieldIcon.gameObject.SetActive(false);
        ShieldCountdownBar.fillAmount = 1f;

        pauseMenu = FindObjectOfType<PauseMenu>();
        gameoverScreen = FindObjectOfType<GameOverScreen>();
    }

    public void TakeDamage(int amount)
    {
        if (isShieldActive)
        {
            isShieldActive = false;
            shieldIcon.gameObject.SetActive(false);
            shieldTimer = shieldTime;
            return;
        }

        //player HIT animation & GAMEOVER
        StartCoroutine(FlickererRB());
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            gameoverScreen.activateGameOverScreen();
            gameObject.SetActive(false);
        }
        for (int i = 0; i < healthContainer.childCount; i++) 
        {
            healthContainer.GetChild(i).gameObject.SetActive(currentHealth > i);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (pauseMenu.IsPaused == true) return;
        //mi prendo il valore dell'imput
        float xMove = Input.GetAxisRaw("Vertical");

        //costruisco il valore movimento
        Vector3 movementVector = new Vector3 (xMove, 0, 0).normalized;

        //applico il movimento
        transform.Translate(movementVector * speed * Time.deltaTime);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -7, 13), transform.position.z);

        //SHOOTING METHOD
        if (ShootCountdown>0)
        {
            ShootCountdown -= Time.deltaTime;
            return;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        { 
            GameObject currbullet=Instantiate(bullet);
            currbullet.transform.position = FirePoint.position;
            ShootCountdown = ShootTime;
        }
        
        //SHIELD
        if (!isShieldActive && shieldTimer <= 0 && Input.GetKey(KeyCode.LeftArrow))
        {
            isShieldActive = true;
            shieldTimer = 0;
            shieldIcon.gameObject.SetActive(true);
        }
        
        //Shield activation and cooldown method
        if (shieldTimer >= 0 && shieldTimer <= shieldTime + shieldCooldown) 
        {
            shieldTimer += Time.deltaTime;
            if (shieldTimer >= shieldTime) 
            {
                isShieldActive = false;
                shieldIcon.gameObject.SetActive(false);
            }

            if (shieldTimer >= shieldTime + shieldCooldown) 
            {
                shieldTimer = -1;
                return;
            }
            ShieldCountdownBar.fillAmount = (shieldTimer - shieldTime) / shieldCooldown;
        }

    }

    //ANIMATION FLICKERER
    private IEnumerator FlickererRB()
    {
        Debug.Log("CoRoutineEnabled");
        animator.SetBool("Hit", true);
        collider2D.enabled = false;

        yield return new WaitForSeconds(invincibilityDuration);

        animator.SetBool("Hit", false);
        collider2D.enabled = true; 
    }
}
