using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AcidPellet : MonoBehaviour {
    [SerializeField] GameObject destructionParticles;
    private void OnTriggerEnter2D(Collider2D collision) {
        if (tag == collision.tag) {
            Enemy e = collision.GetComponent<Enemy>();
            Player p = collision.GetComponent<Player>();
            Instantiate(destructionParticles, transform.position, Quaternion.identity);
            if (e != null) {
                gameObject.SetActive(false);
                e.InflictDamage();
            } else if (p != null) {
                gameObject.SetActive(false);
                if (p.powerDash.isActiveAndEnabled && p.powerDash.inDash) {
                    //Do nothing
                } else
                    p.InflictDamage();
                    AudioManager.instance.Play("Damage");
            }
        } else if (collision.tag == "Obstacle") {
            Instantiate(destructionParticles, transform.position, Quaternion.identity);
            AudioManager.instance.Play("Land");
            gameObject.SetActive(false);
        }
    }
}
