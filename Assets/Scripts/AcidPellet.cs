using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPellet : MonoBehaviour {
    private void OnTriggerEnter2D(Collider2D collision) {
        if (tag == collision.tag) {
            Enemy e = collision.GetComponent<Enemy>();
            Player p = GetComponent<Player>();
            if (e != null) {
                gameObject.SetActive(false);
                e.InflictDamage();
            } else if (p != null) {
                gameObject.SetActive(false);
                p.InflictDamage();
            }
        } else if (collision.tag == "Obstacle") {
            gameObject.SetActive(false);
        }
        Debug.Log("Overlap");
    }
}
