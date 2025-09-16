using UnityEngine;

/// <summary>
/// Singleton manager for playing click sound effects.
/// </summary>
public class ClickSfx : MonoBehaviour
{
    public static ClickSfx Instance;

    private AudioSource _audioSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        _audioSource = GetComponent<AudioSource>();
    }

    public void PlaySound()
    {
        if (_audioSource.isPlaying)
            _audioSource.Stop();
        _audioSource.Play();
    }
}