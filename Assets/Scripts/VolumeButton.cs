using UnityEngine;
using UnityEngine.Audio;

public class VolumeButton : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;

    [SerializeField] private GameObject imageUnmuted;
    [SerializeField] private GameObject imageMuted;

    private bool soundMuted;

    public void ToggleVolumeMute()
    {
        soundMuted = !soundMuted;

        imageMuted.SetActive(soundMuted);
        imageUnmuted.SetActive(!soundMuted);

        var volume = soundMuted ? -80 : 0;
        mixer.SetFloat("Volume", volume);
    }
}
