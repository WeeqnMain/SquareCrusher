using UnityEngine;

public class DestructableManager : MonoBehaviour
{
    [SerializeField] private string nextSceneName;
    [SerializeField] private ScoreView scoreView;
    [SerializeField] private Destructable[] destructables;
    [SerializeField] private int pointsForLight;
    [SerializeField] private int pointsForHeavy;

    private void Awake()
    {
        foreach (var destructable in destructables)
        {
            destructable.Destroyed += OnDestroyedDesctructable;
        }
    }

    private void Update()
    {
        CheckIfSquaresOver();
    }

    private void OnDestroyedDesctructable(bool isHeavy)
    {
        if (isHeavy)
        {
            scoreView.UpdateScore(pointsForHeavy);
        }
        else
        {
            scoreView.UpdateScore(pointsForLight);
        }
    }

    private void CheckIfSquaresOver()
    {
        foreach(var destructable in destructables)
        {
            if (destructable.gameObject.activeSelf)
                return;
        }
        SceneLoader.LoadScene(nextSceneName);
    }
}
