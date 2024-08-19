using UnityEngine;

public class EffectPlaceholder : MonoBehaviour
{
    public bool IsEmpty => currentEffectIcon == null;

    private EffectIcon currentEffectIcon = null;

    public EffectType GetCurrentEffectType() => currentEffectIcon.GetComponent<EffectIcon>().GetEffectType();

    public EffectIcon GetCurrentIcon() => currentEffectIcon;

    public void PutEffectIcon(EffectIcon effectIcon)
    {
        currentEffectIcon = Instantiate(effectIcon, transform);
    }

    public void RemoveEffectIcon()
    {
        if (currentEffectIcon != null)
            Destroy(currentEffectIcon.gameObject);
        currentEffectIcon = null;
    }

    public void SetVisibilityOnCurrentIcon(float value)
    {
        currentEffectIcon.SetVisibility(value);
    }
}