using UnityEngine;
using TMPro;
using System.Collections;

public class StartCountdown : MonoBehaviour
{
    public TextMeshProUGUI countdownText;
    public RisingWater risingWater;

    public float countdownTime = 3f;
    public float delayAfterCountdown = 2f;

    void Start()
    {
        StartCoroutine(BeginSequence());
    }

    IEnumerator BeginSequence()
    {
        float timeLeft = countdownTime;

        while (timeLeft > 0)
        {
            countdownText.text = Mathf.Ceil(timeLeft).ToString();
            yield return new WaitForSeconds(1f);
            timeLeft--;
        }

        countdownText.text = "GO!";
        yield return new WaitForSeconds(1f);

        countdownText.text = "";

        yield return new WaitForSeconds(delayAfterCountdown);

        risingWater.StartRising();
    }
}