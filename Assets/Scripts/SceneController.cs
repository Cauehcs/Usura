using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController instace;

    public bool lockedEscape = false;

    private void Awake() {

        instace = this;

    }

    private void Update() {

        if (Input.GetKeyDown(KeyCode.Escape) && SceneManager.GetActiveScene().name != "Home" && !lockedEscape) {
        
            SceneManager.LoadScene("Home");
        
        }

    }

    public void GoToPomodoro() {

        SceneManager.LoadScene("Pomodoro");

    }

    public void GoToSchedule() {
        
        SceneManager.LoadScene("Schedule");

    }
    public void Exit() {

        SceneManager.LoadScene("Home");

    }
}
