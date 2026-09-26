using UnityEngine;

public class PlayerWaterDeath : MonoBehaviour
{
    public float surviveTimeInWater = 2f;
    public GameManager gameManager;
    public AudioManager audioManager;

    private float waterTimer = 0f;
    private bool inWater = false;

    void Update()
    {
        if (inWater)
        {
            waterTimer += Time.deltaTime;

            if (waterTimer >= surviveTimeInWater)
            {
                if (gameManager != null)
                {
                    gameManager.GameOver();
                }
            }
        }
        else
        {
            waterTimer = 0f;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = true;

            if (audioManager != null)
            {
                audioManager.PlayWaterSplash();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Water"))
        {
            inWater = false;
        }
    }
}