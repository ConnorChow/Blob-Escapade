using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidShot : AbilityTemplate {
    [SerializeField] int existingShots;
    public override void OnStartEnemy() {
        base.OnStartEnemy();
        cooldown = 1;
    }
    public override void OnStartPlayer() {
        base.OnStartPlayer();
    }
    public override void OnUpdatePlayer() {
        base.OnUpdatePlayer();
        //on left button, fire
        if (Input.GetMouseButtonDown(0)) {
            int dir = GetComponent<Player>().trackDirection;
        }
    }
    public override void OnUpdateEnemy() {
        base.OnUpdateEnemy();
    }
}
