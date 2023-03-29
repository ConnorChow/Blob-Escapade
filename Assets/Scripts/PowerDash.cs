using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDash : AbilityTemplate {
    [SerializeField] float dashDuration = 1;
    [SerializeField] float dashBurnout = 1;
    [SerializeField] float dashSpeed = 2000;
    bool inDash = false;
    int direction = 1;
    Rigidbody2D rb;

    float gScale;
    public override void OnStartPlayer() {
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

    public void FixedUpdate() {
        if (inDash) {
            dashBurnout -= Time.fixedDeltaTime;
            rb.velocity = new Vector2(dashSpeed * Time.fixedDeltaTime * direction, 0);
            if (dashBurnout <= 0) {
                dashBurnout = dashDuration;
                inDash = false;
                SetCooldown();
                rb.gravityScale = gScale;
            }
        }
    }
}
