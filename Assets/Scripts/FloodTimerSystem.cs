using UnityEngine;
using TMPro;
using System.Collections;

public class FloodTimerSystem : MonoBehaviour
{
    [Header("References")]
    public RisingWater risingWater;
    public TextMeshProUGUI countdownText;
    public TextMeshProUGUI floodTimerText;
    public TextMeshProUGUI objectiveText;

    [Header("Timing")]
    public float countdownSeconds = 5f;
    public float graceSeconds = 1f;

    [Header("Colours")]
    public Color normalCountdownColour = Color.white;
    public Color warningCountdownColour = Color.red;
    public Color timerColour = Color.cyan;

    private float floodElapsedTime = 0f;
    private bool floodStarted = false;
    private bool systemStopped = false;
    private bool hasStarted = false;

    public AudioManager audioManager;

    void Start()
    {
        floodElapsedTime = 0f;
        floodStarted = false;
        systemStopped = false;
        hasStarted = false;

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }

        if (floodTimerText != null)
        {
            floodTimerText.gameObject.SetActive(false);
        }

        if (objectiveText != null)
        {
            objectiveText.gameObject.SetActive(true);
            objectiveText.text = "Turn off the tap before the bathroom floods";
        }
    }

    void Update()
    {
        if (floodStarted && !systemStopped)
        {
            floodElapsedTime += Time.deltaTime;

            if (floodTimerText != null)
            {
                floodTimerText.text = "Flood Time: " + FormatTime(floodElapsedTime);
            }
        }
    }

    public void BeginSystem()
    {
        if (hasStarted) return;

        hasStarted = true;
        StartCoroutine(BeginFloodSequence());
    }

    IEnumerator BeginFloodSequence()
    {
        float timer = countdownSeconds;
        int lastSecond = -1;

        while (timer > 0f && !systemStopped)
        {
            int currentSecond = Mathf.CeilToInt(timer);

            if (currentSecond != lastSecond)
            {
                lastSecond = currentSecond;

                if (audioManager != null)
                {
                    audioManager.PlayCountdownClick();
                }
            }

            if (countdownText != null)
            {
                countdownText.gameObject.SetActive(true);
                countdownText.text = "Water rises in: " + currentSecond.ToString();

                if (timer <= 3f)
                {
                    countdownText.color = warningCountdownColour;
                }
                else
                {
                    countdownText.color = normalCountdownColour;
                }
            }

            timer -= Time.deltaTime;
            yield return null;
        }

        if (systemStopped)
        {
            yield break;
        }

        if (countdownText != null)
        {
            countdownText.text = "RUN!";
            countdownText.color = warningCountdownColour;
        }

        yield return new WaitForSeconds(graceSeconds);

        if (systemStopped)
        {
            yield break;
        }

        if (countdownText != null)
        {
            countdownText.gameObject.SetActive(false);
        }

        if (floodTimerText != null)
        {
            floodTimerText.gameObject.SetActive(true);
            floodTimerText.color = timerColour;
            floodTimerText.text = "Flood Time: 00:00.0";
        }

        floodStarted = true;

        if (risingWater != null)
        {
            risingWater.StartRising();
        }

        if (audioManager != null)
        {
            audioManager.StartFloodSound();
        }
    }

    public void StopSystem()
    {
        systemStopped = true;
        StopAllCoroutines();

        if (risingWater != null)
        {
            risingWater.StopRising();
        }
    }

    public string GetFormattedFloodTime()
    {
        return FormatTime(floodElapsedTime);
    }

    private string FormatTime(float timeValue)
    {
        int minutes = Mathf.FloorToInt(timeValue / 60f);
        float seconds = timeValue % 60f;
        return minutes.ToString("00") + ":" + seconds.ToString("00.0");
    }
}