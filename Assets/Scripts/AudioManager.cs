using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("Audio sources")]
    [SerializeField] private AudioSource _musicSource;
    [SerializeField] private AudioSource _soundFXSource;

    public static AudioManager Instance;

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
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        if (_musicSource.clip == clip) return;

        _musicSource.clip = clip;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void PlayMusicForced(AudioClip clip)
    {
        _musicSource.clip = clip;
        _musicSource.loop = true;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.clip = null;
        _musicSource.loop = false;
        _musicSource.Stop();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        _soundFXSource.volume = volume;
        _soundFXSource.PlayOneShot(clip);
    }

    public void PlayRandomPitchedSFX(AudioClip clip, float minPitch, float maxPitch)
    {
        _soundFXSource.pitch = Random.Range(minPitch, maxPitch);
        _soundFXSource.PlayOneShot(clip);
    }
}
