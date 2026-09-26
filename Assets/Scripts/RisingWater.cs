using UnityEngine;

public class RisingWater : MonoBehaviour
{
    [Header("Water Rise Settings")]
    public float riseSpeed = 0.15f;
    public bool isRising = false;

    void Update()
    {
        if (isRising)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        }
    }

    public void StartRising()
    {
        isRising = true;
    }

    public void StopRising()
    {
        isRising = false;
    }
}