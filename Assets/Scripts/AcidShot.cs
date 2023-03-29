using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidShot : AbilityTemplate {
    //these shots 
    [SerializeField] GameObject acidShotPrefab; //Prefab for the AcidShot
    [SerializeField] int maximumShots = 15;     //Maximum number of shots player is allowed to make at a time
    private GameObject[] acidShots;             //Recyclable shots
    private int[] direction;                    //Direction that the acid shot moves in

    private float speed = 10;
    public override void OnStartEnemy() {
        base.OnStartEnemy();
        InitializeCooldown(1.5f);

        acidShots = new GameObject[maximumShots];
        direction = new int[maximumShots];

        for (int i = 0; i < maximumShots; i++) {
            acidShots[i] = Instantiate(acidShotPrefab);
            acidShots[i].SetActive(true);
        }
    }
    public override void OnStartPlayer() {
        base.OnStartPlayer();
        InitializeCooldown(1.5f);

        acidShots = new GameObject[maximumShots];
        direction = new int[maximumShots];
        //initialize shots into the scene
        for (int i = 0; i < maximumShots; i++) {
            acidShots[i] = Instantiate(acidShotPrefab);
            acidShots[i].SetActive(false);
            SetCooldown();
        }
    }
    public override void OnUpdatePlayer() {
        base.OnUpdatePlayer();
        //on left button, fire
        if (Input.GetMouseButtonDown(0) && abilityReady) {
            FirePellet();   //Fire the pellet on shot
        }
        UpdateAcid();
    }
    public override void OnUpdateEnemy() {
        base.OnUpdateEnemy();
    }
    private void FirePellet() {
        int dir = GetComponent<Player>().trackDirection;    //get the direction that the player is facing
        for (int i = 0; i < maximumShots; i++) {
            if (!acidShots[i].activeInHierarchy) {
                acidShots[i].SetActive(true);   //Set the player to active
                acidShots[i].transform.position = transform.position;   //set the acid shot's location to the transform of the character 
                direction[i] = dir;             //Set the heading
                SetCooldown();
                break;
            }
        }
    }
    private void UpdateAcid() {
        //Find a shot that isn't being fired into the scene
        for (int i = 0; i < maximumShots; i++) {
            if (acidShots[i].activeInHierarchy) {
                acidShots[i].transform.position = new Vector3(
                    acidShots[i].transform.position.x + direction[i] * Time.deltaTime * speed, 
                    acidShots[i].transform.position.y, 
                    acidShots[i].transform.position.z);
            }
        }
    }
}
