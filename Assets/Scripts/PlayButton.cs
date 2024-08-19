using UnityEngine;

public class PlayButton : MonoBehaviour
{
    [SerializeField] private AudioClip clickSound;

    public void OnButtonClick()
    {
        AudioManager.Instance.PlaySFX(clickSound);
        SceneLoader.LoadScene("Level_1");
    }
}
