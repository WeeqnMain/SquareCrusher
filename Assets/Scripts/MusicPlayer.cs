using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip music;

    private void Awake()
    {
        AudioManager.Instance.PlayMusic(music);
    }
}
