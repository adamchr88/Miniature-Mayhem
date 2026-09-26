using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource musicSource;
    public AudioSource sfxSource;
    public AudioSource floodSource;

    public AudioClip actionMusic;
    public AudioClip countdownClick;
    public AudioClip waterFlood;
    public AudioClip waterSplash;
    public AudioClip jumpSound;

    public AudioClip spiderAlert;

    void Start()
    {
        if (musicSource != null && actionMusic != null)
        {
            musicSource.clip = actionMusic;
            musicSource.loop = true;
            musicSource.Play();
        }

        if (floodSource != null && waterFlood != null)
        {
            floodSource.clip = waterFlood;
            floodSource.loop = true;
        }
    }

    public void PlayCountdownClick()
    {
        PlaySFX(countdownClick);
    }

    public void PlayWaterSplash()
    {
        PlaySFX(waterSplash);
    }

    public void PlayJump()
    {
        PlaySFX(jumpSound);
    }

    public void StartFloodSound()
    {
        if (floodSource != null && !floodSource.isPlaying)
        {
            floodSource.Play();
        }
    }

    public void StopFloodSound()
    {
        if (floodSource != null)
        {
            floodSource.Stop();
        }
    }

    private void PlaySFX(AudioClip clip)
    {
        if (sfxSource != null && clip != null)
        {
            sfxSource.PlayOneShot(clip);
        }
    }

    public void PlaySpiderAlert()
    {
        if (sfxSource != null && spiderAlert != null)
        {
            sfxSource.PlayOneShot(spiderAlert);
        }
    }
}