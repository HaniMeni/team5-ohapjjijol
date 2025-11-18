using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SimulationTimer : MonoBehaviour
{
    public TextMeshProUGUI timerTMP;
    public TextMeshProUGUI scoreText;
    private float startTime = 0f;
    private float pausedTime = 0f;
    private bool isRunning = false;

    public int playerScore = 0;
    void Update()
    {
        if (isRunning)
        {
            float elapsedTime = Time.time - startTime + pausedTime;

            if(elapsedTime >= 600f || playerScore >= 65)
            {
                isRunning = false;
                LoadEndingScene();
            }

            int min = Mathf.FloorToInt(elapsedTime / 60);
            int sec = Mathf.FloorToInt(elapsedTime % 60);
            int millsec = Mathf.FloorToInt((elapsedTime * 1000) % 1000);
            
            if(timerTMP != null)
                timerTMP.text = $"{min:00}:{sec:00}:{millsec:000}";
        }
    }

    public void StartTimer()
    {
        if (isRunning) return;
        startTime = Time.time;
        isRunning = true;
        Debug.Log("타이머 시작: " + startTime);
    }

    public void PauseTimer()
    {
        if (!isRunning) return;
        pausedTime += Time.time - startTime;
        isRunning = false;
    }

    public void ResumeTimer()
    {
        if (isRunning) return;

        startTime = Time.time;
        isRunning = true;
    }

    public void AddScore(int amount)
    {
        playerScore += amount;
        scoreText.text = $"Score: {playerScore}";
    }

    void LoadEndingScene()
    {
        if (playerScore >= 65)
        {
            SceneManager.LoadScene("HappyEnding");
        }
        else
        {
            SceneManager.LoadScene("BadEnding");
        }
    }

}
