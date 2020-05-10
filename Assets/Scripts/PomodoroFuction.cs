using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PomodoroFuction : MonoBehaviour
{
    [SerializeField] float floatMinutesLeft, floatSecondsLeft;
    [SerializeField] float floatTotalHours, floatTotalMinutes, floatTotalSeconds;
    [SerializeField] float periodIndex, standardActivityMinutes, standardRestMinutes;

    [SerializeField] string[] periodsPomodoro;

    public Text minutesLeft, secondsLeft, periods;
    public Text totalHours, totalMinutes, totalSeconds;
    public Text[] inputsConfig;

    public GameObject btnPlay, btnPause, btnStop, btnConfig, btnConfirm;
    public GameObject[] GameObjectsTexts, GameObjectsConfig;

    public AudioSource audioAlarm;
         
    private void Start() {

        audioAlarm.enabled = false;

        standardActivityMinutes = PlayerPrefs.GetFloat("activity");
        standardRestMinutes = PlayerPrefs.GetFloat("rest");

        floatMinutesLeft = standardActivityMinutes; floatSecondsLeft = 1;
        floatTotalHours = 0; floatTotalMinutes = 0; floatTotalSeconds = -1;

        periodIndex = 0;
        Time.timeScale = 0;
        btnPause.SetActive(false);
        btnConfirm.SetActive(false);
        StartCoroutine(Stopwatch());

    }

    private void Update() {
        
        SetText();
        ExtraConfigs();

    }

    void SetText() {

        if(periodIndex == 8) {
            periods.text = "PERÍODO ATUAL: " + periodsPomodoro[2] + " | PRÓXIMO PERIODO: " + periodsPomodoro[0];
        }
            else if(periodIndex == 7) {
                periods.text = "PERÍODO ATUAL: " + periodsPomodoro[1] + " | PRÓXIMO PERIODO: " + periodsPomodoro[2];
            }
               else if (periodIndex % 2 == 1) {
                    periods.text = "PERÍODO ATUAL: " + periodsPomodoro[1] + " | PRÓXIMO PERIODO: " + periodsPomodoro[0];
                }
                    else {
                        periods.text = "PERÍODO ATUAL: " + periodsPomodoro[0] + " | PRÓXIMO PERIODO: " + periodsPomodoro[1];
                    }

        if (floatMinutesLeft >= 10) minutesLeft.text = floatMinutesLeft.ToString();
            else minutesLeft.text = "0" + floatMinutesLeft.ToString();

        if (floatSecondsLeft >= 10) secondsLeft.text = floatSecondsLeft.ToString();
            else secondsLeft.text = "0" + floatSecondsLeft.ToString();

        if (floatTotalSeconds >= 10) totalSeconds.text = ":" + floatTotalSeconds.ToString();
            else totalSeconds.text = ":0" + floatTotalSeconds.ToString();

        if (floatTotalMinutes >= 10) totalMinutes.text = floatTotalMinutes.ToString();
            else totalMinutes.text = "0" + floatTotalMinutes.ToString();
        
        if (floatTotalHours >= 10) totalHours.text = floatTotalHours.ToString() + ":";
            else totalHours.text = "0" + floatTotalHours.ToString() + ":";

    }

    public void ExtraConfigs() {

        if(inputsConfig[0].text == "" || inputsConfig[1].text == "") {

            btnConfirm.GetComponent<Button>().interactable = false;

        }
            else {

                btnConfirm.GetComponent<Button>().interactable = true;

        }

    }

    public void ButtonConfig() {

        btnConfig.SetActive(false); btnConfirm.SetActive(true);
        btnPlay.GetComponent<Button>().interactable = false;
        btnStop.GetComponent<Button>().interactable = false;


        for (int i = 0; i < GameObjectsTexts.Length; i++) {

            GameObjectsTexts[i].SetActive(false);

        }

        for (int i = 0; i < GameObjectsConfig.Length; i++) {

            GameObjectsConfig[i].SetActive(true);

        }

    }
    public void ButtonConfirm() {
        
        btnConfig.SetActive(true); btnConfirm.SetActive(false);
        btnPlay.GetComponent<Button>().interactable = true;
        btnStop.GetComponent<Button>().interactable = true;

        for (int i = 0; i < GameObjectsTexts.Length; i++) {
            GameObjectsTexts[i].SetActive(true);
        }

        for (int i = 0; i < GameObjectsConfig.Length; i++) {
            GameObjectsConfig[i].SetActive(false);
        }

        standardActivityMinutes = float.Parse(inputsConfig[0].text);
        PlayerPrefs.SetFloat("activity" , standardActivityMinutes);

        standardRestMinutes = float.Parse(inputsConfig[1].text);
        PlayerPrefs.SetFloat("rest", standardRestMinutes);

        PlayerPrefs.Save();

        ButtonStop();

    }

    public void ButtonPlay() {
        
        Time.timeScale = 1;
       
        btnConfig.GetComponent<Button>().interactable = false;
        btnPlay.SetActive(false); btnPause.SetActive(true);
        
    }

    public void ButtonPause() {
        Time.timeScale = 0;
        btnPlay.SetActive(true); btnPause.SetActive(false);
    }

    public void ButtonStop() {
        
        periodIndex = 0;
        floatTotalHours = 0; floatTotalMinutes = 0; floatTotalSeconds = 0;
        floatMinutesLeft = standardActivityMinutes; floatSecondsLeft = 0;
        
        btnConfig.GetComponent<Button>().interactable = true;
        btnPlay.SetActive(true); btnPause.SetActive(false);
        
        Time.timeScale = 0;

    }

    IEnumerator Stopwatch() {

        floatSecondsLeft--;
        floatTotalSeconds++;

        if(floatTotalSeconds > 59) {

            floatTotalSeconds = 0;
            floatTotalMinutes++;

        }

        if (floatTotalMinutes > 59) {

            floatTotalMinutes = 0;
            floatTotalHours++;

        }

        if (floatSecondsLeft < 0) {

            floatSecondsLeft = 59;
            floatMinutesLeft--;

        }

        if(floatMinutesLeft < 0) {

            StartCoroutine(AlarmCoroutine());

            if (periodIndex < 8) periodIndex++;
            else if (periodIndex > 7) periodIndex = 0;

            if (periodIndex == 9) {

                periodIndex = 0;

            }
            
                else if (periodIndex == 8) {

                    floatMinutesLeft = (standardActivityMinutes + standardRestMinutes) - 1; floatSecondsLeft = 59;

                }
                    else if(periodIndex % 2 == 1) {

                        floatMinutesLeft = standardRestMinutes - 1; floatSecondsLeft = 59;

                    }
                        else {

                            floatMinutesLeft = standardActivityMinutes - 1; floatSecondsLeft = 59;

                        }

        }

        yield return new WaitForSeconds(1);
        StartCoroutine(Stopwatch());
    }

    IEnumerator AlarmCoroutine() {

        audioAlarm.enabled = true;

        yield return new WaitForSeconds(4);

        audioAlarm.enabled = false;

    }
}
