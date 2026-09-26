using UnityEngine;

public class TapEffectsController : MonoBehaviour
{
    public ParticleSystem tapStream;
    public ParticleSystem waterSplash;
    public ParticleSystem drips;

    public void StopEffects()
    {
        if (tapStream != null)
        {
            tapStream.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        if (waterSplash != null)
        {
            waterSplash.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }

        if (drips != null)
        {
            drips.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        }
    }

    public void StartEffects()
    {
        if (tapStream != null)
        {
            tapStream.Play();
        }

        if (waterSplash != null)
        {
            waterSplash.Play();
        }

        if (drips != null)
        {
            drips.Play();
        }
    }
}