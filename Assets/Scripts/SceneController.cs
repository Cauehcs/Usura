using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Home") {
            SceneManager.LoadScene("Home");
        }
    }

    public void GoToPomodoro() {
        SceneManager.LoadScene("Pomodoro");
    }

    public void Exit() {
        SceneManager.LoadScene("Home");
    }
}
