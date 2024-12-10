using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip pickupSound;
    public AudioClip winSound;

    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPickupSound()
    {
        PlaySound(pickupSound);
    }

    public void PlayWinSound()
    {
        PlaySound(winSound);
    }
    
    void PlaySound(AudioClip _newSound)
    {
        // This sets the audio sources audio clip to be passed in sound
        audioSource.clip = _newSound;
        // This then plays the audiosource
        audioSource.Play();
    }

}
