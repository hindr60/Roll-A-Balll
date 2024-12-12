using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float currentTime;
    float bestTime;
    public bool timing = false;

    SceneController sceneController;

    [Header("UI Countdown Panel")]
    public GameObject countdownPanel;
    public TMP_Text countdownText;

    [Header("UI In Game Panel")]
    public TMP_Text timerText;

    [Header("UI Game Over Panel")]
    public GameObject timesPanel;
    public TMP_Text winTimeText;
    public TMP_Text bestTimeText;

    void Start()
    {
        timesPanel.SetActive(false);
        countdownPanel.SetActive(false);
        //timerText.text = "";
        sceneController = FindObjectOfType<SceneController>();
     
    }

    public IEnumerator StartCountdown()
    {
        yield return new WaitForEndOfFrame();
        if (PlayerPrefs.HasKey("BestTime"))
            bestTime = PlayerPrefs.GetFloat("BestTime" + sceneController.GetSceneName());
        else
            bestTime = 1000f;

        countdownPanel.SetActive(true);
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "Go!";
        yield return new WaitForSeconds(1);
        StartTimer();
        countdownPanel.SetActive(false);
    }
    void Update()
    {
     
       if(timing)
        {
            currentTime += Time.deltaTime;
       
        }
       
    }

    public float GetTime()
    {
        return currentTime;
    }

    public void StartTimer()
    {
        timing = true;
    }
    public void StopTimer()
    { 
        timing = false;
        timesPanel.SetActive(true);
        timerText.text = currentTime.ToString("F3");
        bestTimeText.text = bestTime.ToString("F3");

        if (currentTime <= bestTime)
        {
            bestTime = currentTime;
            PlayerPrefs.SetFloat("BestTime" + sceneController.GetSceneName(), bestTime);
            bestTimeText.text = " Personal best: " + bestTime.ToString("F3");
        }
    }
    public bool IsTiming()
    {
        return timing;
    }
}
