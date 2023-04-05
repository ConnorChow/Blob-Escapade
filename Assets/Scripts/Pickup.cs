using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {
    public bool isItDash = false;
    public AcidType acidType = 0;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Player") {
            Player playerScript = collision.gameObject.GetComponent<Player>();
            if (playerScript != null) {
                if (isItDash) {
                    if (playerScript.powerDash.isActiveAndEnabled) {
                        playerScript.powerDash.UpgradeDash();
                    } else {
                        playerScript.powerDash.enabled = true;
                        AudioManager.instance.Play("Pickup");
                    }
                } else {
                    if (playerScript.acidShot.isActiveAndEnabled) {
                        playerScript.acidShot.UpgradeAcid(acidType);
                    } else {
                        playerScript.acidShot.enabled = true;
                        AudioManager.instance.Play("Pickup");
                    }
                }
                Destroy(gameObject);
            }
        }
    }
}
