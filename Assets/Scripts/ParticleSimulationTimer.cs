using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSimulationTimer : MonoBehaviour {
    [SerializeField] float delay = 2.5f;
    void Start() {
        Destroy(gameObject, delay);
    }
}
