using UnityEngine;
using UnityEngine.UI;

public abstract class EffectIcon : MonoBehaviour
{
    [SerializeField] private EffectType effectType;

    [SerializeField] protected Image[] showImages;

    [SerializeField] protected Image[] backgroundImages;

    public EffectType GetEffectType() => effectType;

    public abstract void SetVisibility(float value);
}
