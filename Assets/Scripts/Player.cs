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
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float JumpSpeed = 10;
    bool canJump = false;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update() {
        //*************Color based on health*************
        color = new Color(1-((float)health/ (float)maxHealth), (float)health / (float)maxHealth, 0);
        playerLight.color = color;
        playerSprite.color = color;

        //Player Movement
        rb.velocity = (new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y));

        //Player Fire
        if (rb.velocity.y == 0) {
            canJump = true;
        } else if (rb.velocity.y < 0) {
            canJump = false;
        }

        if (Input.GetKeyDown(KeyCode.Space) && canJump) {
            rb.AddForce(new Vector2(0, JumpSpeed));
            canJump = false;
        }
    }
}
