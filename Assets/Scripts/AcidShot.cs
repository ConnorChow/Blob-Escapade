using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AcidType {
    Basic,
    ForwardAndUp,
    FourWay,
    OctaShot
}

public class AcidShot : AbilityTemplate {
    //these shots 
    [SerializeField] GameObject acidShotPrefab; //Prefab for the AcidShot
    [SerializeField] int maximumShots = 15;     //Maximum number of shots player is allowed to make at a time
    private GameObject[] acidShots;             //Recyclable shots
    private Vector2Int[] direction;             //Direction that the acid shot moves in
    
    [SerializeField] AcidType acidType;         //power of acid that the ability

    private float speed = 10;
    public override void OnStartEnemy() {
        base.OnStartEnemy();
        InitializeCooldown(1.5f);

        acidShots = new GameObject[maximumShots];
        direction = new Vector2Int[maximumShots];
        //initialize shots into the scene
        for (int i = 0; i < maximumShots; i++) {
            acidShots[i] = Instantiate(acidShotPrefab);
            acidShots[i].SetActive(false);
            acidShots[i].tag = "Player";
            SetCooldown();
        }
    }
    public override void OnStartPlayer() {
        base.OnStartPlayer();
        InitializeCooldown(1.5f);

        acidShots = new GameObject[maximumShots];
        direction = new Vector2Int[maximumShots];
        //initialize shots into the scene
        for (int i = 0; i < maximumShots; i++) {
            acidShots[i] = Instantiate(acidShotPrefab);
            acidShots[i].SetActive(false);
            acidShots[i].tag = "Enemy";
            SetCooldown();
        }
    }
    public override void OnUpdatePlayer() {
        base.OnUpdatePlayer();
        //on left button, fire
        if (Input.GetMouseButtonDown(0) && abilityReady) {
            FireType(GetComponent<Player>().trackDirection);
        }
        UpdateAcid();
    }
    public override void OnUpdateEnemy() {
        base.OnUpdateEnemy();
        //Detect the player if nearby
        if (Vector2.Distance(transform.position, GetComponent<Enemy>().player.transform.position) < detectionRadius && !GetComponent<Enemy>().player.GetComponent<Player>().dead) {
            GetComponent<Enemy>().attackMode = true;
        } else GetComponent<Enemy>().attackMode = false;

        if (abilityReady && GetComponent<Enemy>().attackMode) {
            int dir = -1;
            if (GetComponent<Enemy>().player.transform.position.x > transform.position.x) dir = 1; else dir = -1;
            FireType(dir);
        }
        UpdateAcid();
    }

    void FireType(int dir) {
        switch (acidType) {
            case AcidType.Basic:
                FirePellet(new Vector2Int(dir, 0));
                break;
            case AcidType.ForwardAndUp:
                FirePellet(new Vector2Int(dir, 0));
                FirePellet(new Vector2Int(0, 1));
                break;
            case AcidType.FourWay:
                FirePellet(new Vector2Int(1, 0));
                FirePellet(new Vector2Int(-1, 0));
                FirePellet(new Vector2Int(0, -1));
                FirePellet(new Vector2Int(0, 1));
                break;
            case AcidType.OctaShot:
                FirePellet(new Vector2Int(1, 0));
                FirePellet(new Vector2Int(-1, 0));
                FirePellet(new Vector2Int(0, -1));
                FirePellet(new Vector2Int(0, 1));
                FirePellet(new Vector2Int(1, -1));
                FirePellet(new Vector2Int(-1, -1));
                FirePellet(new Vector2Int(-1, 1));
                FirePellet(new Vector2Int(1, 1));
                break;
        }
    }

    private void FirePellet(Vector2Int heading) {
        for (int i = 0; i < maximumShots; i++) {
            if (!acidShots[i].activeInHierarchy) {
                acidShots[i].SetActive(true);       //Set the player to active
                acidShots[i].transform.position = transform.position;   //set the acid shot's location to the transform of the character
                Vector2 dir = heading;
                if (dir.x == dir.y) {
                    dir.x /= 1.5f;
                    dir.y /= 1.5f;
                }
                float angle = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg - 90;
                acidShots[i].transform.rotation = Quaternion.Euler(0, 0, angle);
                direction[i] = heading;             //Set the heading
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
                    acidShots[i].transform.position.x + direction[i].x * Time.deltaTime * speed, 
                    acidShots[i].transform.position.y + direction[i].y * Time.deltaTime * speed, 
                    acidShots[i].transform.position.z);
            }
        }
    }
    public void UpgradeAbility(AcidType newAcidType) {
        if (newAcidType > acidType) {
            acidType = newAcidType;
        }
    }

    private void OnDestroy() {
        for (int i = 0; i < maximumShots; i++) {
            Destroy(acidShots[i].gameObject);
        }
    }
}
