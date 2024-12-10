using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip pickupSound;
    public AudioClip winSound;

    public AudioSource audioSource;
    public AudioSource audioSource2;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayPickupSound()
    {
        PlaySound(pickupSound, audioSource);
    }

    public void PlayWinSound()
    {
        PlaySound(winSound, audioSource2);
    }
    
    void PlaySound(AudioClip _newSound, AudioSource _audioSource)
    {
        // This sets the audio sources audio clip to be passed in sound
        _audioSource.clip = _newSound;
        // This then plays the audiosource
        _audioSource.Play();
    }

}
