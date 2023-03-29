using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {
    GameObject player;

    [SerializeField] int maxHealth = 3;
    public int health = 3;

    public bool dead = false;

    // Start is called before the first frame update
    void Start() {
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update() {
        if (dead) Destroy(this);
    }

    public void InflictDamage() {
        health -= 1;
        if (health < 0) dead = true;
    }
}
