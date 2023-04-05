using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RestartGameScript : MonoBehaviour {
    Button button;
    // Start is called before the first frame update
    void Start() {
        button = GetComponent<Button>();
        button.onClick.AddListener(ResetGame);
    }

    void ResetGame() {
        SceneManager.LoadScene(0);
    }
}
