using UnityEngine;

public class MusicScript : MonoBehaviour
{
    public AudioSource _audioSource;
    public AudioSource _winAudioSource;
    private void Awake()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    public void PlayLosingMusic()
    {
        if (_audioSource.isPlaying) return;
        _audioSource.Play();
    }

    public void PlayWinningMusic()
    {
        if (_winAudioSource.isPlaying) return;
        _winAudioSource.Play();
    }

    public void StopMusic()
    {
        _audioSource.Stop();
    }
}