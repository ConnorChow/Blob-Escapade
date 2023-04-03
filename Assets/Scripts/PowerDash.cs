using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDash : AbilityTemplate {
    [SerializeField] float dashDuration = 1;
    [SerializeField] float dashBurnout = 1;
    [SerializeField] float dashSpeed = 2000;
    public bool inDash = false;
    int direction = 1;
    Rigidbody2D rb;

    float gScale;
    public override void OnStartPlayer() {
        base.OnStartPlayer();
        rb = GetComponent<Rigidbody2D>();
        gScale = rb.gravityScale;
    }
    public override void OnStartEnemy() {
        base.OnStartPlayer();
        rb = GetComponent<Rigidbody2D>();
        gScale = rb.gravityScale;
    }

    public override void OnUpdatePlayer() {
        base.OnUpdatePlayer();
        if (Input.GetKeyDown(KeyCode.LeftShift) && abilityReady) {
            Debug.Log("Dash");
            inDash = true;
            direction = GetComponent<Player>().trackDirection;
            rb.gravityScale = 0;
        }
    }

    public override void OnUpdateEnemy() {
        base.OnUpdateEnemy();
        if (abilityReady && Mathf.Abs(transform.position.x - GetComponent<Enemy>().player.transform.position.x) < 3 && Mathf.Abs(transform.position.y - GetComponent<Enemy>().player.transform.position.y) < 0.1) {
            GetComponent<Enemy>().attackMode = true;
        } else if (inDash) {
            GetComponent<Enemy>().attackMode = true;
        } else {
            GetComponent<Enemy>().attackMode = false;
        }
        if (!abilityReady) {
            GetComponent<Enemy>().attackMode = false;
        }
        if (GetComponent<Enemy>().attackMode && abilityReady && !inDash) {
            if (transform.position.x < GetComponent<Enemy>().player.transform.position.x) {
                direction = 1;
            } else {
                direction = -1;
            }
            inDash = true;
            rb.gravityScale = 0;
        }
    }

    public void UpgradeDash() {
        dashDuration *= 1.1f;
        UpgradeCooldown();
    }

    public void FixedUpdate() {
        if (inDash) {
            dashBurnout -= Time.fixedDeltaTime;
            rb.velocity = new Vector2(rb.velocity.x, 0);
            rb.AddForce(new Vector2(dashSpeed * Time.fixedDeltaTime * direction, 0));
            if (dashBurnout <= 0) {
                dashBurnout = dashDuration;
                inDash = false;
                SetCooldown();
                rb.gravityScale = gScale;
            }
        }
    }

    [SerializeField] GameObject DashDrop;
    private void OnDestroy() {
        Instantiate(DashDrop, transform.position, Quaternion.identity);
    }
}