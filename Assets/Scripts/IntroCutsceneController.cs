using UnityEngine;
using System.Collections;
using StarterAssets;

public class IntroCutsceneController : MonoBehaviour
{
    public Camera introCamera;
    public Camera playerCamera;
    public FirstPersonController playerController;
    public FloodTimerSystem floodTimerSystem;

    public float introDuration = 8.3f;

    void Start()
    {
        if (introCamera != null)
        {
            introCamera.gameObject.SetActive(true);
        }

        if (playerCamera != null)
        {
            playerCamera.gameObject.SetActive(false);
        }

        if (playerController != null)
        {
            playerController.enabled = false;
        }

        StartCoroutine(PlayIntro());
    }

    IEnumerator PlayIntro()
    {
        yield return new WaitForSeconds(introDuration);

        if (introCamera != null)
        {
            introCamera.gameObject.SetActive(false);
        }

        if (playerCamera != null)
        {
            playerCamera.gameObject.SetActive(true);
        }

        if (playerController != null)
        {
            playerController.enabled = true;
        }

        if (floodTimerSystem != null)
        {
            floodTimerSystem.BeginSystem();
        }
    }
}