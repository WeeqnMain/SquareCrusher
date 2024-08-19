using TMPro;
using UnityEngine;

public class BombEffectIcon : EffectIcon
{
    [SerializeField] private TextMeshProUGUI bombCountText;

    public int BombCount { get; private set; }

    public override void SetVisibility(float value)
    {
        throw new System.NotImplementedException();
    }

    public void IncreaseBombCount(int value = 1)
    {
        BombCount += value;
        bombCountText.text = $"{BombCount}";
    }

    public void DecreaseBombCount(int value = 1)
    {
        BombCount -= value;
        bombCountText.text = $"{BombCount}";
    }
}