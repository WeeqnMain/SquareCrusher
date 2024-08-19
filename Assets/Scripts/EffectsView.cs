using System.Collections;
using UnityEngine;

public class EffectsView : MonoBehaviour
{
    [SerializeField] private EffectPlaceholder[] placeholders;
    [SerializeField] private EffectIcon[] effectIconPrefabs;

    private Coroutine sizeRoutine;
    private Coroutine expRoutine;

    public void AddBombEffect(EffectType effectType)
    {
        var bombIcon = GetIconByType(effectType);
        if (placeholders[2].IsEmpty)
        {
            placeholders[2].PutEffectIcon(bombIcon);
        }
        var currentBombPlace = (BombEffectIcon)placeholders[2].GetCurrentIcon();
        currentBombPlace.IncreaseBombCount();
    }

    public void RemoveBombEffect()
    {
        var currentBombPlace = (BombEffectIcon)placeholders[2].GetCurrentIcon();
        currentBombPlace.DecreaseBombCount();
        if (currentBombPlace.BombCount <= 0)
        {
            placeholders[2].RemoveEffectIcon();
        }
    }

    /// <summary>
    /// <b>Only for not bombs effect :(</b>
    /// </summary>
    public void AddEffect(EffectType effectType, float duration)
    {
        var effectIcon = GetIconByType(effectType);
        foreach (EffectPlaceholder placeholder in placeholders)
        {
            if (placeholder.IsEmpty || placeholder.GetCurrentEffectType() == effectType)
            {
                placeholder.RemoveEffectIcon();
                placeholder.PutEffectIcon(effectIcon);

                switch (effectType)
                {
                    case EffectType.SizeIncrease:
                        if (sizeRoutine != null)
                        {
                            StopCoroutine(sizeRoutine);
                        }
                        sizeRoutine = StartCoroutine(SizeRoutine(placeholder, duration));
                        break;
                    case EffectType.ExperienceMultiplier:
                        if (expRoutine != null)
                        {
                            StopCoroutine(expRoutine);
                        }
                        expRoutine = StartCoroutine(ExpRoutine(placeholder, duration));
                        break;
                }
                break;
            }
        }
    }

    private IEnumerator SizeRoutine(EffectPlaceholder placeholder, float duration)
    {
        float time = duration;
        while (time > 0)
        {
            placeholder.SetVisibilityOnCurrentIcon(time / duration);
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        placeholder.RemoveEffectIcon();
    }

    private IEnumerator ExpRoutine(EffectPlaceholder placeholder, float duration)
    {
        float time = duration;
        while (time > 0)
        {
            placeholder.SetVisibilityOnCurrentIcon(time / duration);
            time -= Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        placeholder.RemoveEffectIcon();
    }

    private EffectIcon GetIconByType(EffectType effectType)
    {
        foreach (EffectIcon icon in effectIconPrefabs)
        {
            if (icon.GetEffectType() == effectType)
                return icon;
        }
        throw new System.Exception($"Can not find {effectType} EffectIcon in prefabs");
    }
}