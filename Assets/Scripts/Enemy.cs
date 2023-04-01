using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Enemy : MonoBehaviour {
    public GameObject player;

    //Local component information
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] Light2D playerLight;

    //Data for displaying and tracking health
    [SerializeField] int maxHealth = 3;
    public int health = 3;
    Color color = new Color(0, 1, 0);

    //Animation controller
    Animator acm;

    //rigidbody from the enemy
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
    [SerializeField] bool dead = false;

    public bool attackMode = false;
    [SerializeField] GameObject patrolPoint1;
    public float d1;
    [SerializeField] GameObject patrolPoint2;
    public float d2;

    public float currentTarget;

    [SerializeField] float detectionRadius = 1;
    [SerializeField] float attackSpeedBuffer = 0;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player");
        rb = GetComponent<Rigidbody2D>();
        acm = GetComponent<Animator>();
        d1 = patrolPoint1.transform.position.x;
        d2 = patrolPoint2.transform.position.x;
        currentTarget = d1;
    }

    // Update is called once per frame
    void Update() {
        if (playerLight == null || playerSprite == null) return;

        //Color based on health
        color = new Color(1 - ((float)health / (float)maxHealth), 0, (float)health / (float)maxHealth);
        playerLight.color = color;
        playerSprite.color = color;

        //Patrol behaviour
        if (!attackMode && Mathf.Abs(currentTarget - rb.position.x) < 0.1f) {
            if (currentTarget == d1)
                currentTarget = d2;
            else if (currentTarget == d2)
                currentTarget = d1;
        }
        //Detect the player if nearby
        if (Vector2.Distance(transform.position, player.transform.position) < detectionRadius && !player.GetComponent<Player>().dead) {
            attackMode = true;
        } else attackMode = false;

        //track driection facing
        if (rb.velocity.x > 0) {
            trackDirection = 1;
        } else if (rb.velocity.x < 0) {
            trackDirection = -1;
        }

        //Control animation
        acm.SetBool("jump", !canJump);

        if (health < 0) {
            Die();
        }
    }
    private void FixedUpdate() {
        if (playerLight == null || playerSprite == null) return;

        //Player Movement
        if (!attackMode) {
            float move = (currentTarget - transform.position.x);
            if (move > 0) move = 1;
            else if (move < 0) move = -1;
            move *= (moveSpeed * Time.fixedDeltaTime);
            rb.velocity = new Vector2(move, rb.velocity.y);
        } else {
            rb.velocity = Vector2.zero;
        }
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

    private void Die() {
        Instantiate(deathParticles, transform.position, Quaternion.identity);
        Destroy(GetComponentInParent<Transform>().gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        Player p = collision.gameObject.GetComponent<Player>();
        if (p != null && !dead) {
            bool inDash = p.gameObject.GetComponent<PowerDash>().inDash;
            if (inDash) Die();
            else p.InflictDamage();
        }
    }
    public void InflictDamage() {
        health -= 1;
        if (health < 0) dead = true;
    }
}
