using UnityEngine;

public class WetSurfaceZone : MonoBehaviour
{
    [Header("Slip Settings")]
    public Vector3 slideDirection = Vector3.forward;
    public float slideStrength = 3f;

    private void OnTriggerEnter(Collider other)
    {
        StarterAssets.FirstPersonController controller = other.GetComponent<StarterAssets.FirstPersonController>();

        if (controller != null)
        {
            controller.EnterWetSurface(slideDirection.normalized, slideStrength);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        StarterAssets.FirstPersonController controller = other.GetComponent<StarterAssets.FirstPersonController>();

        if (controller != null)
        {
            controller.ExitWetSurface();
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawRay(transform.position, slideDirection.normalized * 2f);
    }
}