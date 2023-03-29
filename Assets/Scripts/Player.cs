using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum AcidType {
    Basic,
    ForwardAndUp,
    FourWay,
    OctaShot
}

public class Player : MonoBehaviour {
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] Light2D playerLight;
    [SerializeField] int maxHealth = 100;
    public int health = 100;
    Color color = new Color(0, 1, 0);

    Animator acm;
    
    Rigidbody2D rb;

    [SerializeField] float moveSpeed = 5;
    [SerializeField] float JumpSpeed = 10;

    bool canJump = false;

    public int trackDirection = 1;

    bool dead = false;

    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        acm = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        //**************************Color based on health**************************
        color = new Color(1-((float)health/ (float)maxHealth), (float)health / (float)maxHealth, 0);
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
            //canJump = false;
        }

        //Control animation
        acm.SetBool("jump", !canJump);

        if (dead) Destroy(rb);
    }

    private void FixedUpdate() {
        //Player Movement
        Vector2 oldVelocity = rb.velocity;

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
        health -= 1;
        if (health < 0) dead = true;
    }
}
