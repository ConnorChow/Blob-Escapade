using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {
    //Local component information
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] Light2D playerLight;

    //Data for displaying and tracking health
    [SerializeField] int maxHealth = 100;
    public int health = 100;
    float healthBuffer = 1;
    public bool isBuffering = false;
    Color color = new Color(0, 1, 0);

    //Animation controller
    Animator acm;

    //rigidbody from the player
    Rigidbody2D rb;

    //move and jumping speed for player
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float JumpSpeed = 10;

    //Boolean check for if the player can jump
    bool canJump = false;

    //track the direction the player is facing
    public int trackDirection = 1;

    //Death-related data
    [SerializeField] GameObject deathParticles;
    public bool dead = false;

    // Game over screen
    public GameObject GameOverScreen;
    public GameObject PlayerUI;

    // To go back to start of level
    public Button restartLevel;
    // To go back to start of game
    public Button restartGame;


    [SerializeField] Slider powerDashSlider;
    public PowerDash powerDash;
    [SerializeField] Slider acidShotSlider;
    public AcidShot acidShot;

    // Key and door
    public GameObject keyInstance;
    public GameObject doorInstance;
    public string nextScene;
    private bool hasKey = false;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        acm = GetComponent<Animator>();

        GameOverScreen.SetActive(false);

        restartGame.onClick.AddListener(RestartGame);
        restartLevel.onClick.AddListener(RestartLevel);

        powerDash = GetComponent<PowerDash>();
        acidShot = GetComponent<AcidShot>();

        // Spawn key and door
        //keyInstance = GameObject.Find("key");//Instantiate(keyPrefab, new Vector3(-15, -5, 0), Quaternion.identity);
        //doorInstance = GameObject.Find("door");//Instantiate(doorPrefab, new Vector3(12, -4, 0), Quaternion.identity);

    }

    // Update is called once per frame
    void Update() {
        if (playerLight == null || playerSprite == null) return;

        //Color based on health
        color = new Color(1 - ((float)health / (float)maxHealth), (float)health / (float)maxHealth, 0);
        playerLight.color = color;
        playerSprite.color = color;

        //track driection facing
        if (rb.velocity.x > 0) {
            trackDirection = 1;
        } else if (rb.velocity.x < 0) {
            trackDirection = -1;
        }

        //Player jump
        if (Input.GetKeyDown(KeyCode.Space) && canJump) {
            rb.AddForce(new Vector2(0, JumpSpeed));
        }

        //Control animation
        acm.SetBool("jump", !canJump);

        //health buffer cooldown
        if (isBuffering) {
            healthBuffer -= Time.deltaTime;
            if (healthBuffer < 0) {
                isBuffering = false;
            }
        }

        //Kill player if dead
        if (health < 0) {
            Instantiate(deathParticles, transform.position, Quaternion.identity);
            dead = true;
            Destroy(playerLight);
            Destroy(playerSprite);
            rb.simulated = false;
            GameOverScreen.SetActive(true);
            PlayerUI.SetActive(false);
        }

        //Display Cooldowns for scripts
        acidShotSlider.gameObject.SetActive(true);
        if (!acidShot.abilityReady) {
            acidShotSlider.value = 1 - (acidShot.cooldownTimer / acidShot.cooldown);
        } else {
            acidShotSlider.value = 1;
        }

        if (powerDash.isActiveAndEnabled) {
            powerDashSlider.gameObject.SetActive(true);
            if (!powerDash.abilityReady) {
                powerDashSlider.value = 1 - (powerDash.cooldownTimer / powerDash.cooldown);
            } else {
                powerDashSlider.value = 1;
            }
        } else {
            powerDashSlider.gameObject.SetActive(false);
        }

        // If player picks up key, do something cool
        if (keyInstance == null) {
            Debug.Log("YOU HAVE NOTHING");
        }
        if (!hasKey && keyInstance != null && Vector3.Distance(transform.position, keyInstance.transform.position) < 1) {
            Debug.Log("Player picked up the key!");
            // Play a cool particle effect
            //Instantiate(pickupEffect, keyInstance.transform.position, Quaternion.identity);

            // Destroy the key instance
            Destroy(keyInstance);

            // Set hasKey to true
            hasKey = true;

            // Play a sound effect
            //AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        // If player reaches the door with the key, load next scene
        if (hasKey && Vector3.Distance(transform.position, doorInstance.transform.position) < 1) {
            // Load the next scene
            SceneManager.LoadScene(nextScene);
        }

    }

    private void FixedUpdate() {
        if (playerLight == null || playerSprite == null) return;

        //Player Movement
        Vector2 move = new Vector2(Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime, rb.velocity.y);
        rb.velocity = move;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        canJump = true;
    }
    private void OnTriggerStay2D(Collider2D collision) {
        canJump = true;
    }
    private void OnTriggerExit2D(Collider2D other) {
        canJump = false;
    }
    public void InflictDamage() {
        if (isBuffering) return;
        health -= 1;
        if (health < 0) dead = true;
        isBuffering = true;
        healthBuffer = 1;
    }

    public void RestartLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void RestartGame() {
        Debug.Log("Restart Game");
    }
}
