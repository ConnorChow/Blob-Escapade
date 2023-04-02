using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityTemplate : MonoBehaviour {
    [SerializeField] private float cooldown;
    protected float cooldownTimer;
    protected bool abilityReady = true;
    protected bool isPlayer;
    protected bool isValid;
    [SerializeField] protected float detectionRadius = 1;
    public void Start() {
        //Try to figure out whether this component is added to an enemy or the player,
        //if anything valid at all
        isValid = true;
        if (GetComponent<Enemy>() != null) {
            isPlayer = false;
            //run a custom start for the enemy
            OnStartEnemy();
        } else if (GetComponent<Player>() != null) {
            isPlayer = true;
            //run a custom start for the player
            OnStartPlayer();
        } else {
            isValid = false;
        }
    }
    public virtual void OnStartEnemy() {

    }
    public virtual void OnStartPlayer() {

    }
    public void Update() {
        //only run updates when the data is connected to a valid game object
        if (isValid) {
            //run cooldown
            if (!abilityReady) {
                cooldownTimer -= Time.deltaTime;
                if (cooldownTimer <= 0) {
                    abilityReady = true;
                }
            }
            //run an update for either the player or the enemy
            if (isPlayer) {
                //only run player update if alive
                if (!GetComponent<Player>().dead) OnUpdatePlayer();
            } else {
                OnUpdateEnemy();
            }
        }
    }
    public virtual void OnUpdatePlayer() {

    }
    public virtual void OnUpdateEnemy() {

    }
    protected void SetCooldown() {
        cooldownTimer = cooldown;
        abilityReady = false;
    }

    public void InitializeCooldown(float cooldown) {
        this.cooldown = cooldown;
    }
    public bool UpgradeCooldown() {
        if (cooldown > 0.33) {
            cooldown *= 0.9f;
            return true;
        } else {
            return false;
        }
    }
}
