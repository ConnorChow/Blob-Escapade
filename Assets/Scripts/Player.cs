using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Player : MonoBehaviour {
    [SerializeField] SpriteRenderer playerSprite;
    [SerializeField] Light2D playerLight;
    [SerializeField] float maxHealth = 100;
    public float health = 100;
    Color color = new Color(0, 1, 0);
    Rigidbody2D rb;
    [SerializeField] float moveSpeed = 5;
    [SerializeField] float JumpSpeed = 10;
    // Start is called before the first frame update
    void Start() {
        rb = GetComponent<Rigidbody2D>();
        //rb.freezeRotation = true;
    }

    // Update is called once per frame
    void Update() {
        //*************Color based on health*************
        color = new Color(1-(health/maxHealth), health/maxHealth, 0);
        playerLight.color = color;
        playerSprite.color = color;

        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * moveSpeed, rb.velocity.y);
        if (Input.GetKeyDown(KeyCode.Space) && rb.velocity.y == 0) {
            rb.AddForce(new Vector2(0, JumpSpeed));
        }
    }
}
