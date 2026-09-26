using UnityEngine;

public class TapInteract : MonoBehaviour
{
    public GameManager gameManager;

    private bool playerInRange = false;
    private bool hasBeenUsed = false;

    void Update()
    {
        if (playerInRange && !hasBeenUsed && Input.GetKeyDown(KeyCode.E))
        {
            hasBeenUsed = true;

            if (gameManager != null)
            {
                gameManager.WinGame();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}